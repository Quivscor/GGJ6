using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    
    public float throwSpeed = 200f;
    public string _type = "simple";
    private int _score;
    private int ownerPLayer = 9999;

    public string Type
    {
        set => _type = value;

        get
        {
            return _type;
        }
    }
    public int Score
    {
        set => _score = value;
        get
        {
            return _score;
        }
    }
    public bool _isPickedUp = false;
    public bool _isThrown = false;

    private GameObject _sprite;

    private void Start()
    {
        _sprite = this.gameObject.transform.Find("Sprite").gameObject;
        if(_sprite == null)
        {
            //Debug.Log("There is no sprite child for this NPC or it's not named Sprite!");
        }
    }
    public void OnPickUp(int _newOwner)
    {
        ownerPLayer = _newOwner;
        _isPickedUp = true;
    }
    public void OnThrow(Vector2 direction)
    {
        _isPickedUp = false;
        _isThrown = true;

        this.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * throwSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            if (collision.gameObject.GetComponent<ThrowScript>().playerNumber != ownerPLayer)
            {
                if (_isThrown)
                {
                    collision.gameObject.SendMessage("OnGettingHit", _type);
                }

                if (_isPickedUp == false && _isThrown == false)
                {
                    ownerPLayer = collision.GetComponent<ThrowScript>().playerNumber;
                    //Debug.Log("NPC can be picked up by " + ownerPLayer.ToString());
                }
            }
            
           

        }
        if(collision.gameObject.CompareTag("Map"))
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            _isThrown = false;
            ownerPLayer = 99999;
            Remove();
        }

        void Remove()
        {
            GameObject.Find("WaveManager").SendMessage("RemoveNPC", this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
