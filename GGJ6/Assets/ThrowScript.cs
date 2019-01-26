﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR.WSA.Input;


public class ThrowScript : MonoBehaviour
{
    public bool holdsSth = false;
    public GameObject HeldGameObject= null;
    public int playerNumber = 0;
    public bool _isStunned = false;

    private MovementController movementController = null;
    // Start is called before the first frame update
    void Start()
    {
        movementController = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Fire1")&&!_isStunned)
        {
            if (holdsSth == false)
            {
                Collider2D[] colliders = new Collider2D[10];
                Collider2D myCollider = gameObject.GetComponent<Collider2D>();
                ContactFilter2D contactFilter = new ContactFilter2D();
                contactFilter.NoFilter();

                int size = myCollider.OverlapCollider(contactFilter, colliders);
                for (int i = 0; i < size; i++)
                {
                    if (colliders[i].gameObject.CompareTag("NPC"))
                    {
                        if (colliders[i].gameObject.GetComponent<NPC>()._isThrown == false)
                        {
                            //Debug.Log("Got NPC");
                            NPC npcScript = colliders[i].GetComponent<NPC>();
                            if (npcScript._isThrown == false && npcScript._isPickedUp == false)
                            {
                                HeldGameObject = colliders[i].gameObject;
                                HeldGameObject.SendMessage("OnPickUp",playerNumber);
                                HeldGameObject.GetComponent<Rigidbody2D>().simulated = false;
                                HeldGameObject.transform.position = this.gameObject.transform.position;
                                HeldGameObject.transform.SetParent(this.gameObject.transform, true);
                                HeldGameObject.transform.Translate(new Vector3(0.0f, 0.05f, 0.0f));
                                holdsSth = true;
                                break;
                            }
                        }
                    }
                }
            }
            else if(holdsSth == true)
            {
                //Debug.Log("Player" + playerNumber+ "throws held NPC");
                HeldGameObject.transform.position = this.gameObject.transform.position;
                HeldGameObject.GetComponent<Rigidbody2D>().simulated = true;
                HeldGameObject.transform.SetParent(this.gameObject.transform.parent, true);
                HeldGameObject.SendMessage("OnThrow",movementController.directionVector2);
                holdsSth = false;
                HeldGameObject = null;

            }
        }
            
        
    }
    public void OnGettingHit(string type)
    {
        //Debug.Log("Player hit by " + type);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

    }

    IEnumerator GetStunned(float seconds)
    {

        _isStunned = true;
        yield return new WaitForSeconds(seconds);
        _isStunned = false;
    }

    
}