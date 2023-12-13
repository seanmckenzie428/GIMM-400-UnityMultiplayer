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
    private Transform t;
    private RaceManager raceManager;
    
    public void Start()
    {
        raceManager = GameObject.Find("RaceManager").GetComponent<RaceManager>();
        t = gameObject.GetComponent<Transform>();
        Invoke(nameof(SetSpawn), 0.01f);
    }
    
    public void SetSpawn()
    {
        var spawnPoint = raceManager.spawnPoints[id - 1];
        gameObject.GetComponent<Transform>().position = spawnPoint.position;
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
