using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    AudioManager audioManager;
    public delegate void ScoreChange();
    public static event ScoreChange OnChanged;

    public int BigCheerAmount = 0;

    public int Score { get; set; }

    void UpdateScore(int amount)
    {
        if ((Score + amount) >= 0)
        {
            Score += amount;
            if(amount > 0)
                audioManager.Play("Points");
            if(Score - BigCheerAmount >= 300)
            {
                audioManager.Play("Cheer1");
                BigCheerAmount += 300;
            }
        }
        else
        {
            Score = 0;
        }
    }

    public bool hasFemale;
    public bool hasChild;
    public bool hasMale;
    public bool hasPet;

    // is there a complete set of family members?
    // types: pet, female, male, child
    public bool isFullSet;

    public bool IsFullSet()
    {
        return isFullSet;
    }

    void UpdateHousehold(string typeString)
    {
        switch (typeString)
        {
            case "female":
                hasFemale = true;
                break;
            case "child":
                hasChild = true;
                break;
            case "male":
                hasMale = true;
                break;
            case "pet":
                hasPet = true;
                break;
            default:
                break;
        }

        if (hasFemale && hasMale && hasChild && hasPet)
        {
            isFullSet = true;
            hasFemale = hasMale = hasChild = hasPet = false;
            UpdateScore(200);
            audioManager.Play("Cheer" + Random.Range(1, 2).ToString());
        }
    }

    public GameObject Owner;

    void UpdateStats(NPC npc)
    {
        UpdateHousehold(npc.Type);
        UpdateScore(npc.Score);

        OnChanged?.Invoke();

        Debug.Log(Score);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            Debug.Log("Hit da house!");
            UpdateStats(collision.gameObject.GetComponent<NPC>());
            collision.gameObject.GetComponent<NPC>().Remove();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        isFullSet = false;

        hasFemale = false;
        hasChild  = false;
        hasMale   = false;
        hasPet    = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
