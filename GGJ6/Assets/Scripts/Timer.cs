using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float timeInSeconds = -3;
    public string time = "00:00";

    public bool stopTime = false;

    // Update is called once per frame
    void Update()
    {
        if(!stopTime)
        {
            timeInSeconds += Time.deltaTime;
            int minutes = (int)timeInSeconds / 60;
            string fillerZero = ((int)timeInSeconds % 60) > 9 ? "" : "0";
            time = minutes.ToString() + ":" + fillerZero + ((int)Mathf.Abs(timeInSeconds) % 60).ToString();
            //Debug.Log(time);
        }
    }
}
