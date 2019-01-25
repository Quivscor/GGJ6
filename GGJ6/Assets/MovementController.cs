using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Rigidbody2D body;
    public Vector2 directionVector2;
    private float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * speed, 1f),Mathf.Lerp(0, Input.GetAxis("Vertical") * speed, 1f));
        //body.AddForce(new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * speed, 1f),Mathf.Lerp(0, Input.GetAxis("Vertical") * speed, 1f)));
        if (Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f)
        {
            directionVector2 = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        }
        
    }
}
