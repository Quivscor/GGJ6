using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class NPC : MonoBehaviour
{
    
    public float throwSpeed = 400f;
    public string _type = "simple";
    private Vector2 drunkDirection= Vector2.zero;
    private int _score = 50;
    public int ownerPLayer = 9999;
    public int _bouncesLeft = 3;
    private float waveSpeed = 5.0f;
    public float waveLength = 0.1f;
    Vector2 perpendicularVector=Vector2.zero;
    public float waving = 0f;
    public bool waveLeft = true;
    DadAnimator animator;
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
        animator = GetComponentInChildren<DadAnimator>();
        _sprite = this.gameObject.transform.Find("Sprite").gameObject;
        if(_sprite == null)
        {
            //Debug.Log("There is no sprite child for this NPC or it's not named Sprite!");
        }
        if (Type == "useless"|| Type=="uselessFat" || Type == "uselessDrunk")
            Score = 0;
    }

    void Update()
    {
        //zDebug.Log(Mathf.Sqrt(this.gameObject.transform.position.x* this.gameObject.transform.position.x + this.gameObject.transform.position.y* this.gameObject.transform.position.y));
        if (Mathf.Abs(this.gameObject.transform.position.x) > 10.0f|| Mathf.Abs(this.gameObject.transform.position.y) > 10.0f)
        {
            Remove();
        }
        if (_isThrown && _type == "uselessDrunk")
        {
            if (waveLeft)
            {
                waving += waveSpeed * Time.deltaTime;
                if (waving >= 1f)
                {
                    waveLeft = false;
                }
            }
            else
            {
                waving -= waveSpeed * Time.deltaTime;
                if (waving <= -1f)
                {
                    waveLeft = true;
                }
            }
            this.gameObject.transform.position += new Vector3(perpendicularVector.x * waving * waveLength, perpendicularVector.y * waving * waveLength,0);
            Debug.Log(new Vector3(perpendicularVector.x * waving * waveLength, perpendicularVector.y * waving * waveLength, 0));
        }
    }

    public void OnPickUp(int _newOwner)
    {
        ownerPLayer = _newOwner;
        _isPickedUp = true;
        if (animator != null)
        {
            animator.SendMessage("PickedUp");
        }
        
    }
    public void OnThrow(Vector2 direction)
    {
        drunkDirection = direction;
        perpendicularVector = new Vector2(- drunkDirection.y,drunkDirection.x);
        Debug.Log(perpendicularVector);


        _isPickedUp = false;
        _isThrown = true;
        if (animator != null)
        {
            animator.SendMessage("Thrown", direction);
        }
 
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * throwSpeed);
    }
    public void OnDrop()
    {
        _isPickedUp = false;
        _isThrown = false;
        ownerPLayer = 99999;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            

            collision.gameObject.SendMessage("OnGettingHit", _type);
            Bounce();
        }
        else
        {
            Bounce();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           

            if (collision.gameObject.GetComponent<ThrowScript>().playerNumber != ownerPLayer)
            {
                if (_type == "uselessFat" && _isThrown)
                {
                    Bounce();
                    collision.gameObject.SendMessage("OnGettingHit", _type);
                }
                if (_isThrown)
                {
                    collision.gameObject.SendMessage("OnGettingHit", _type);
                }

                if (_isPickedUp == false && _isThrown == false)
                {
                    ownerPLayer = collision.GetComponent<ThrowScript>().playerNumber;
                    //Debug.Log("NPC can be picked up by " + ownerPLayer.ToString());
                }
                if ((Type == "male" || Type == "female" || Type == "child" || Type == "pet") && _isThrown)
                {
                    Remove();
                }
            }

            



        }
        if(collision.gameObject.CompareTag("Map"))
        {
            if (_type != "uselessFat")
            {
                this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                _isThrown = false;
                ownerPLayer = 99999;
                Remove();
            }
            else
            {
                Bounce();
            }


        }
    }
    public void Remove()
    {
        GameObject.Find("WaveManager").SendMessage("RemoveNPC", this.gameObject);
        Destroy(this.gameObject);
    }

    public void Bounce()
    {

        if (_bouncesLeft > 0)
        {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            ownerPLayer = 99999;
            _bouncesLeft--;
        }
        else
        {
            Remove();
        }
    }
}
