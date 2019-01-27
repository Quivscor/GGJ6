using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void PlayersAndHousesSet();
    public static event PlayersAndHousesSet OnHouseReady;

    public Timer timer;
    public static GameManager GM;

    public float preGameTime;
    bool lockCountdown = false;

    public string endCondition;

    public Text victoryText;

    public List<House> houses;
    //public List<ThrowScript> players;
    public List<GameObject> players;
    private void Awake()
    {
        if (GM == null)
            GM = this;
        else if (GM != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        timer = GetComponent<Timer>();
        if(houses == null)
        {
            Debug.Log("No houses added!");
        }
        if(players == null)
        {
            Debug.Log("No players added");
        }
        //victoryText = GameObject.Find("VictoryText").GetComponent<Text>();
        if(victoryText == null)
        {
            Debug.Log("No VictoryText on map!");
        }
        if(endCondition == "")
        {
            Debug.Log("There is no end condition");
        }

        InitGame();
    }

    private void Update()
    {
        if (!lockCountdown)
        {
            if (preGameTime > 2)
            {
                victoryText.text = "3!";
            }
            if (preGameTime < 2 && preGameTime > 1)
            {
                victoryText.text = "2!";
            }
            if (preGameTime < 1 && preGameTime > 0)
            {
                victoryText.text = "1!";
            }
            if (preGameTime < 0 && !lockCountdown)
            {
                victoryText.text = "Go!";
                foreach (GameObject player in players)
                {
                    player.GetComponent<ThrowScript>()._isStunned = false;
                }
            }
            if (preGameTime < -1)
            {
                //victoryText.text = "";
                victoryText.enabled = false;
                lockCountdown = true;
            }
        }

        if (endCondition == timer.time)
        {
            EndGame();
        }

        if(!lockCountdown)
            preGameTime -= Time.deltaTime;
    }

    void InitGame()
    {

        players[0].GetComponentInChildren<SpriteRenderer>().color = Color.red;
        players[0].GetComponent<ThrowScript>()._isStunned = true;
        //houses[0].Owner = players[0];
        players[1].GetComponentInChildren<SpriteRenderer>().color = Color.green;
        players[1].GetComponent<ThrowScript>()._isStunned = true;
       // houses[1].Owner = players[1];
        players[2].GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
        players[2].GetComponent<ThrowScript>()._isStunned = true;
        //houses[2].Owner = players[2];
        players[3].GetComponentInChildren<SpriteRenderer>().color = Color.blue;
        players[3].GetComponent<ThrowScript>()._isStunned = true;
       // houses[3].Owner = players[3];
        //OnHouseReady?.Invoke();
    }

    void EndGame()
    {
        int bestScore = 0;
        int bestScoreHouseId = 0;
        foreach(House house in houses)
        {
            if(house.Score > bestScore)
            {
                bestScore = house.Score;
                bestScoreHouseId = house.Owner.GetComponent<ThrowScript>().playerNumber;
                victoryText.color = house.GetComponent<SpriteRenderer>().color;
            }
        }
        victoryText.text = "Player " + bestScoreHouseId + " won!\nScore: " + bestScore;
        
        victoryText.enabled = true;

        StartCoroutine(FinishGame());
    }

    IEnumerator FinishGame()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(0);
    }
}
