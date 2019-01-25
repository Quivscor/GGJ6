using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<Spawner> spawners;
    public List<GameObject> activeNPCs;

    public float respawnCooldown = 10;
    float _currentCooldown = 0;

    private void Update()
    {
        if(!CheckIfWorthyNPCs())
        {
            _currentCooldown += Time.deltaTime;
            if(_currentCooldown >= respawnCooldown)
            {
                Respawn();
                _currentCooldown = 0;
            }
        }
    }

    public void Respawn()
    {
        int uselessCount = 0;
        foreach(Spawner spawner in spawners)
        {
            if(uselessCount >= 2)
                activeNPCs.Add(spawner.Spawn(true));
            else
                activeNPCs.Add(spawner.Spawn());
            string usefulness = activeNPCs[activeNPCs.Count - 1].GetComponent<NPC>().Type;
            if (usefulness[0] == 'u')
                uselessCount++;
        }
    }

    bool CheckIfWorthyNPCs()
    {
        foreach(GameObject npc in activeNPCs)
        {
            string foo = npc.GetComponent<NPC>().Type;
            if (!(foo[0] == 'u'))
                return true;
        }
        return false;
    }
}
