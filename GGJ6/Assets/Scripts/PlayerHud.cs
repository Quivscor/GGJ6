using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{
    public House House { get; set; }

    public Text playerName;
    public Text playerScore;

    private Image hasMale;
    private Image hasFemale;
    private Image hasChild;
    private Image hasPet;

    void UpdateHudData()
    {
        playerScore.text = House.Score.ToString();

        if(House.hasChild)
            hasChild.sprite = Resources.Load<Sprite>("FamilyIcons/d1");
        if (House.hasMale)
            hasMale.sprite = Resources.Load<Sprite>("FamilyIcons/t1");
        if (House.hasFemale)
            hasFemale.sprite = Resources.Load<Sprite>("FamilyIcons/m1");
        if (House.hasPet)
            hasPet.sprite = Resources.Load<Sprite>("FamilyIcons/p1");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitDisplayData()
    {
        Component[] texts;

        texts = gameObject.GetComponentsInChildren(typeof(Text));

        if (texts != null)
        {
            foreach (Text text in texts)
            {
                if (text.name == "PlayerName") playerName = text;
                if (text.name == "Score") playerScore = text;
            }
        }
        else
        {
            // Try again, looking for inactive GameObjects
            Component[] textsInactive = gameObject.GetComponentsInChildren(typeof(Text), true);

            foreach (Text text in textsInactive)
            {
                if (text.name == "PlayerName") playerName = text;
                if (text.name == "Score") playerScore = text;
            }
        }

        playerName.text = "Player " + House.Owner.playerNumber;
        playerName.color = House.GetComponent<SpriteRenderer>().color;
        playerScore.text = 0.ToString();

        Component[] familyIcons;

        familyIcons = gameObject.GetComponentsInChildren(typeof(Image));

        if (familyIcons != null)
        {
            foreach (Image familyIcon in familyIcons)
            {
                if (familyIcon.name == "ChildImage") hasChild = familyIcon;
                if (familyIcon.name == "MaleImage") hasMale = familyIcon;
                if (familyIcon.name == "FemaleImage") hasFemale = familyIcon;
                if (familyIcon.name == "PetImage") hasPet = familyIcon;
            }
        }
        else
        {
            // Try again, looking for inactive GameObjects
            Component[] familyIconsInactive = gameObject.GetComponentsInChildren(typeof(Image), true);

            foreach (Image familyIcon in familyIconsInactive)
            {
                if (familyIcon.name == "ChildImage") hasChild = familyIcon;
                if (familyIcon.name == "MaleImage") hasMale = familyIcon;
                if (familyIcon.name == "FemaleImage") hasFemale = familyIcon;
                if (familyIcon.name == "PetImage") hasPet = familyIcon;
            }
        }

        //hasChild.sprite = Resources.Load<Sprite>("FamilyIcons/d1");
        //hasMale.sprite = Resources.Load<Sprite>("FamilyIcons/t1");
        //hasFemale.sprite = Resources.Load<Sprite>("FamilyIcons/m1");
        //hasPet.sprite = Resources.Load<Sprite>("FamilyIcons/p1");
        //gameObject.SetActive(false);
        //gameObject.SetActive(true);
    }

    void OnEnable()
    {
        House.OnChanged += UpdateHudData;
        //GameManager.OnHouseReady += InitDisplayData;
    }

    // Not sure if needed ???
    void OnDisable()
    {
        House.OnChanged -= UpdateHudData;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
