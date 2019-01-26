using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    
    public Rigidbody2D body;
    public Vector2 directionVector2;
    private float speed = 2.0f;
    private ThrowScript throwScript= null;
    public bool isMoving = false;
    public bool _invertMovement= false;

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
            Vector2 temVel = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal" + throwScript.playerNumber) * speed, 1f), Mathf.Lerp(0, Input.GetAxis("Vertical" + throwScript.playerNumber) * speed, 1f));
            if (throwScript.playerNumber > 2)
            {
                if (temVel == Vector2.zero)
                {
                    temVel = new Vector2(Mathf.Lerp(0, Input.GetAxisRaw("Horizontal" + throwScript.playerNumber + "Key") * speed, 1f), Mathf.Lerp(0, Input.GetAxisRaw("Vertical" + throwScript.playerNumber + "Key") * speed, 1f));
                    Debug.Log("wololo" + throwScript.playerNumber);
                }
            }
            if (!_invertMovement)
            {
                
                body.velocity = temVel;
               
                if (temVel != Vector2.zero)
                {
                    isMoving = true;
                    directionVector2 = temVel.normalized;
                }
                else if (temVel == Vector2.zero)
                {
                    isMoving = false;
                }
            }
            else
            {
                body.velocity = -temVel;
                if (temVel != Vector2.zero)
                {
                    isMoving = true;
                    directionVector2 = temVel.normalized;
                }
                else if (temVel==Vector2.zero)
                {
                    isMoving = false;
                }
            }
            
        }
        else
        {
            body.velocity=Vector2.zero;
        }
        

    }

   
}
