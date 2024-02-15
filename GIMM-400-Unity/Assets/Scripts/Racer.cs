using System;
using UnityEngine;

public class Racer : MonoBehaviour
{
  public Material[] playerMaterials;
    public GameObject[] coloredObjects;
    [NonSerialized]
    public int id;
    [NonSerialized]
    public int lapsCompleted;
    private bool hitCheckpoint = false;
    private Transform lastCheckpoint;
    private Transform t;
    private RaceManager raceManager;

    public float baseSpeed = 5f; // Initial speed of the player
    private float currentSpeed; // Current speed of the player

    public void Start()
    {
        if (coloredObjects.Length > 0)
        {
            foreach (GameObject obj in coloredObjects)
            {
                obj.GetComponent<Renderer>().material = playerMaterials[id - 1];
            }
        }

        raceManager = GameObject.Find("RaceManager").GetComponent<RaceManager>();
        t = gameObject.GetComponent<Transform>();
        Invoke(nameof(SetSpawn), 0.2f);
        currentSpeed = baseSpeed;
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
        else if (other.CompareTag("BoostPickup"))
        {
            BoostPickup boostPickup = other.GetComponent<BoostPickup>();
            if (boostPickup != null)
            {
                IncreaseSpeed(boostPickup.boostAmount);
                other.gameObject.SetActive(false); // Deactivate the boost pickup
            }
        }
    }

    public void IncreaseSpeed(float amount)
    {
        currentSpeed += amount; // Increase player's speed
    }

    // Other methods and functionalities of the racer...
}