using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    private int score = 0;

    public int GetScore()
    {
        return score;
    }

    void UpdateScore(int amount)
    {
        if ((score - amount) >= 0)
        {
            score += amount;
        }

    }

    private bool hasFemale;
    private bool hasChild;
    private bool hasMale;
    private bool hasPet;

    // is there a complete set of family members?
    // types: pet, female, male, child
    private bool isFullSet;

    bool IsFullSet()
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
        }
    }

    public Player Owner { get; set; }

    void UpdateStats(NPC npc)
    {
        UpdateHousehold(npc.GetType());
        UpdateScore(npc.GetScore());
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "NPC")
        {
            UpdateStats(collision.gameObject.GetComponent<NPC>());
            Destroy(collision.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isFullSet = false;

        hasFemale = false;
        hasChild  = false;
        hasMale   = false;
        hasPet    = false;

        Owner = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
