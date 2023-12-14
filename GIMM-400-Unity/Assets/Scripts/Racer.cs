using System;
using UnityEngine;

public class Racer : MonoBehaviour
{
    public Color[] playerColors;
    public GameObject[] coloredObjects;
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
        if (coloredObjects.Length > 0)
        {
            foreach (GameObject obj in coloredObjects)
            {
                obj.GetComponent<Renderer>().material.color = playerColors[id - 1];
            }
        }

        raceManager = GameObject.Find("RaceManager").GetComponent<RaceManager>();
        t = gameObject.GetComponent<Transform>();
        Invoke(nameof(SetSpawn), 0.1f);
    }

    public void SetSpawn()
    {
        t = gameObject.GetComponent<Transform>();
        var spawnPoint = raceManager.spawnPoints[id - 1];
        t.position = spawnPoint.position;
        t.rotation = spawnPoint.rotation;
    }

    public void CompleteLap()
    {
        hitCheckpoint = false;
        lapsCompleted++;
        raceManager.CheckForWin();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            if (hitCheckpoint)
            {
                CompleteLap();
            }
        }
        else if (other.CompareTag("Checkpoint"))
        {
            hitCheckpoint = true;
        }
    }
}
