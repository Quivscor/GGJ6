using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    
    Rigidbody2D body;
    public Vector2 directionVector2;
    private float speed = 2.0f;
    private ThrowScript throwScript= null;

    public GameObject pickedUpPerson;
    // Start is called before the first frame update
    void Start()
    {
        throwScript = GetComponent<ThrowScript>();
        body = GetComponent<Rigidbody2D>();
        //Debug.Log("Horizontal" + throwScript.playerNumber);
    }

    // Update is called once per frame
    void Update()
    {
        if (!throwScript._isStunned)
        {
            body.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal" + throwScript.playerNumber) * speed, 1f), Mathf.Lerp(0, Input.GetAxis("Vertical" + throwScript.playerNumber) * speed, 1f));
            if (Input.GetAxis("Horizontal"+ throwScript.playerNumber) != 0.0f || Input.GetAxis("Vertical" + throwScript.playerNumber) != 0.0f)
            {
                directionVector2 = new Vector2(Input.GetAxis("Horizontal" + throwScript.playerNumber), Input.GetAxis("Vertical" + throwScript.playerNumber)).normalized;
            }
        }
        

    }

   
}
