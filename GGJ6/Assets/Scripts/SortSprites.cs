using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortSprites : MonoBehaviour
{
    void LateUpdate()
    {
        GetComponent<SpriteRenderer>().sortingOrder = -(int)(transform.position.y * 100);
    }
}
