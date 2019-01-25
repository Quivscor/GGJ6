﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float throwSpeed;

    private string _type;
    private int _score;

    public string Type
    {
        private set => _type = value;
        get
        {
            return _type;
        }
    }
    public int Score
    {
        private set => _score = value;
        get
        {
            return _score;
        }
    }
    private bool _isPickedUp = false;
    private bool _isThrown = false;
    private GameObject _holder;

    private GameObject _sprite;

    private void Start()
    {
        _sprite = this.gameObject.transform.Find("Sprite").gameObject;
        if(_sprite == null)
        {
            Debug.Log("There is no sprite child for this NPC or it's not named Sprite!");
        }
    }
    public void OnPickUp(GameObject player)
    {
        _holder = player;
        _isPickedUp = true;
    }
    public void OnThrow(Vector2 direction)
    {
        _isPickedUp = false;
        _isThrown = false;

        this.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * throwSpeed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("OnGettingHit", _type);
        }
        if(collision.gameObject.CompareTag("Map"))
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}