using System;
using UnityEngine;

public class Racer : MonoBehaviour
{
    [NonSerialized]
    public int id;
    [NonSerialized]
    public int lapsCompleted;
    
    private bool hitCheckpoint = false;
    private Transform lastCheckpoint; 
    
    private RaceManager raceManager;

    public void SetSpawn(Transform spawnPoint)
    {
        transform.position = spawnPoint.position;
    }

    public void CompleteLap()
    {
        hitCheckpoint = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            if (hitCheckpoint)
            {
                CompleteLap();
            }
        } else if (other.CompareTag("Checkpoint"))
        {
            hitCheckpoint = true;
        }
    }
}
