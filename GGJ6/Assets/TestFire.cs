using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFire : MonoBehaviour
{
    public NPC testObject;
    public Vector2 direction;
    
    public void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            testObject.OnThrow(direction);
            Debug.Log("Fire");
        }
    }
}
