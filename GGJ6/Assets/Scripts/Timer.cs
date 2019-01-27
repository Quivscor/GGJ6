using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float timeInSeconds = -3;
    public string time = "00:00";

    Text timerLabel;
    int endConditionTimeInSeconds;

    string timeUntil = "00:00";

    float currentTimeUntilTheEnd;

    public bool stopTime = false;

    void Start()
    {
        var endConditionSplitted = GameManager.GM.endCondition.Split(':');

        // minutes
        endConditionTimeInSeconds = Int32.Parse(endConditionSplitted[0].TrimStart('0').PadLeft(1, '0')) * 60;
        // add seconds
        endConditionTimeInSeconds += Int32.Parse(endConditionSplitted[1].TrimStart('0').PadLeft(1, '0'));

        currentTimeUntilTheEnd = endConditionTimeInSeconds;

        timerLabel = GameObject.FindGameObjectWithTag("TimerLabel").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!stopTime)
        {
            timeInSeconds += Time.deltaTime;
            int minutes = (int)timeInSeconds / 60;
            string fillerZero = ((int)timeInSeconds % 60) > 9 ? "" : "0";
            time = minutes.ToString() + ":" + fillerZero + ((int)Mathf.Abs(timeInSeconds) % 60).ToString();

            if (currentTimeUntilTheEnd >= 0.0f)
            {
                currentTimeUntilTheEnd -= Time.deltaTime;
                minutes = (int)currentTimeUntilTheEnd / 60;
                fillerZero = ((int)currentTimeUntilTheEnd % 60) > 9 ? "" : "0";
                timeUntil = minutes.ToString() + ":" + fillerZero + ((int)Mathf.Abs(currentTimeUntilTheEnd) % 60).ToString();
            }

            timerLabel.text = timeUntil;

            //Debug.Log(time);
        }
    }
}
