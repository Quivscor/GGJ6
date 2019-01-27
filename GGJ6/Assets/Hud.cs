using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud : MonoBehaviour
{
    House[] houses = new House[4];

    public GameObject hud1;
    public GameObject hud2;
    public GameObject hud3;
    public GameObject hud4;

    void OnEnable()
    {
        //GameManager.OnHouseReady += InitHuds;
    }

    // Not sure if needed ???
    void OnDisable()
    {
        //GameManager.OnHouseReady -= InitHuds;
    }

    void InitHuds()
    {
        //houses = GameObject.FindObjectsOfType<House>();

        //if (hud1 != null && houses.Length >= 1)
        //{
        //    foreach(House h in houses)
        //    {
        //        if(h.Owner.playerNumber == 4)
        //        {
        //            hud1.GetComponent<PlayerHud>().House = h;
        //            hud1.GetComponent<PlayerHud>().InitDisplayData();
        //        }
        //    }
        //}

        //if (hud2 != null && houses.Length >= 2)
        //{
        //    foreach (House h in houses)
        //    {
        //        if (h.Owner.playerNumber == 1)
        //        {
        //            hud2.GetComponent<PlayerHud>().House = h;
        //            hud2.GetComponent<PlayerHud>().InitDisplayData();
        //        }
        //    }
        //}

        //if (hud3 != null && houses.Length >= 3)
        //{
        //    foreach (House h in houses)
        //    {
        //        if (h.Owner.playerNumber == 3)
        //        {
        //            hud3.GetComponent<PlayerHud>().House = h;
        //            hud3.GetComponent<PlayerHud>().InitDisplayData();
        //        }
        //    }
        //}

        //if (hud3 != null && houses.Length >= 4)
        //{
        //    foreach (House h in houses)
        //    {
        //        if (h.Owner.playerNumber == 2)
        //        {
        //            hud4.GetComponent<PlayerHud>().House = h;
        //            hud4.GetComponent<PlayerHud>().InitDisplayData();
        //        }
        //    }
        //}

        // if there is no house for a player, hide UI
        //if (hud1.GetComponent<PlayerHud>().House == null) hud1.SetActive(false);
        //if (hud2.GetComponent<PlayerHud>().House == null) hud2.SetActive(false);
        //if (hud3.GetComponent<PlayerHud>().House == null) hud3.SetActive(false);
        //if (hud4.GetComponent<PlayerHud>().House == null) hud4.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
