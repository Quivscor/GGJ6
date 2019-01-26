﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public delegate void PlayersAndHousesSet();
    public static event PlayersAndHousesSet OnHouseReady;

    public Timer timer;
    public static GameManager GM;

    public string endCondition;

    public Text victoryText;

    House[] houses = new House[4];

    private void Awake()
    {
        if (GM == null)
            GM = this;
        else if (GM != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        timer = GetComponent<Timer>();
        houses = GameObject.FindObjectsOfType<House>();
        if(houses == null)
        {
            Debug.Log("No houses on map!");
        }
        victoryText = GameObject.Find("VictoryText").GetComponent<Text>();
        if(victoryText == null)
        {
            Debug.Log("No VictoryText on map!");
        }

        InitGame();
    }

    private void Update()
    {
        if (endCondition == timer.time)
        {
            EndGame();
        }
    }

    void InitGame()
    {
        ThrowScript[] players = GameObject.FindObjectsOfType<ThrowScript>();
        for(int i = 0; i < houses.Length; i++)
        {
            houses[i].Owner = players[i];
            switch(i)
            {
                case 0:
                    players[i].GetComponentInChildren<SpriteRenderer>().color = Color.red;
                    break;
                case 1:
                    players[i].GetComponentInChildren<SpriteRenderer>().color = Color.blue;
                    break;
                case 2:
                    players[i].GetComponentInChildren<SpriteRenderer>().color = Color.green;
                    break;
                case 3:
                    players[i].GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
                    break;
            }
            
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
    }
}
