using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> spawnerList;
    public int uselessCount = 0;
    public ParticleSystem spawnSmoke;

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
        if (spawnSmoke != null)
            spawnSmoke.Play();
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("Spawn");
        return Instantiate(spawnerList[index], new Vector3(this.transform.position.x + Random.Range(-1.0f,1.0f),
            this.transform.position.y + Random.Range(-2.0f,2.0f),this.transform.position.z), Quaternion.identity);
    }
}
