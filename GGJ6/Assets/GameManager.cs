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
    public List<ThrowScript> players;

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
        victoryText = GameObject.Find("VictoryText").GetComponent<Text>();
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
        if(preGameTime > 2)
        {
            victoryText.text = "3!";
        }
        if(preGameTime < 2 && preGameTime > 1)
        {
            victoryText.text = "2!";
        }
        if (preGameTime < 1 && preGameTime > 0)
        {
            victoryText.text = "1!";
        }
        if (preGameTime < 0)
        {
            victoryText.text = "Go!";
            foreach(ThrowScript player in players)
            {
                player._isStunned = false;
            }
        }
        if(preGameTime < -1)
        {
            victoryText.text = "";
            lockCountdown = true;
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
        int i = 0;
        foreach(House house in houses)
        { 
            switch(i)
            {
                case 0:
                    players[i].GetComponentInChildren<SpriteRenderer>().color = Color.red;
                    players[i]._isStunned = true;
                    break;
                case 1:
                    players[i].GetComponentInChildren<SpriteRenderer>().color = Color.blue;
                    players[i]._isStunned = true;
                    break;
                case 2:
                    players[i].GetComponentInChildren<SpriteRenderer>().color = Color.green;
                    players[i]._isStunned = true;
                    break;
                case 3:
                    players[i].GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
                    players[i]._isStunned = true;
                    break;
            }
            i++;
        }
        OnHouseReady?.Invoke();
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
            }
        }
        victoryText.text = "Player " + bestScoreHouseId + " won!\nScore: " + bestScore;
        victoryText.enabled = true;

        StartCoroutine(FinishGame());
    }

    IEnumerator FinishGame()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}
