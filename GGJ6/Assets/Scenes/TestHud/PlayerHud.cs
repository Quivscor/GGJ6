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

    void UpdateScoreText()
    {
        playerScore.text = House.Score.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);

        //hasChild = GameObject.Find("ChildImage").GetComponent<Image>();
        //hasChild.sprite = Resources.Load("Sprites/FamilyIcons/d2.png") as Sprite;
    }

    public void InitDisplayData()
    {
        Component[] texts;

        texts = GetComponentsInChildren(typeof(Text));

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
            Component[] textsInactive = GetComponentsInChildren(typeof(Text), true);

            foreach (Text text in textsInactive)
            {
                if (text.name == "PlayerName") playerName = text;
                if (text.name == "Score") playerScore = text;
            }
        }

        playerName.text = "Player " + House.Owner.playerNumber;
        playerScore.text = 0.ToString();

        gameObject.SetActive(true);
    }

    void OnEnable()
    {
        House.OnChanged += UpdateScoreText;
        //GameManager.OnHouseReady += InitDisplayData;
    }

    // Not sure if needed ???
    void OnDisable()
    {
        House.OnChanged -= UpdateScoreText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
