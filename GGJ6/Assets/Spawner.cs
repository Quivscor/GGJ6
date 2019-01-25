using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> spawnerList;
    public int uselessCount = 0;

    public GameObject Spawn(bool forceUsefull = false)
    {
        int useless = 0;
        if(forceUsefull)
        {
            useless += uselessCount;
        }
        Random random = new Random();
        int index = Random.Range(0, spawnerList.Count - useless);
        Debug.Log(index);
        return Instantiate(spawnerList[index]);
    }
}
