﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{

    public delegate void ScoreChange();
    public static event ScoreChange OnChanged;

    public int Score { get; set; }

    void UpdateScore(int amount)
    {
        if ((Score + amount) >= 0)
        {
            Score += amount;
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
    private bool isFullSet;

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
            //isFullSet = true;
            hasFemale = hasMale = hasChild = hasPet = false;
            Score += 200;
        }
    }

    public ThrowScript Owner { get; set; }

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
