using System;
using System.Linq;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Racer : MonoBehaviour
{
    public Material[] playerMaterials;
    public GameObject[] coloredObjects;
    [NonSerialized] public int id;
    [NonSerialized] public int lapsCompleted;
    public Rigidbody rb;
    public RaceManager raceManager;
    [CanBeNull] public Player player;
    [SerializeField]
    private TrailRenderer trailRenderer;

    private bool hitCheckpoint = false;
    private Transform lastCheckpoint;
    private Transform t;

    public void Start()
    {
        t = gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        player = GetComponentInParent<Player>();
        trailRenderer.emitting = false;
        // Invoke(nameof(SetSpawn), 0.2f);
    }

    public void SetPlayerColor()
    {
        if (coloredObjects.Length > 0)
        {
            foreach (GameObject obj in coloredObjects)
            {
                obj.GetComponent<Renderer>().material = playerMaterials[id - 1];
            }

            if (trailRenderer != null)
            {
                var mat = trailRenderer.material;
                var playerMat = playerMaterials[id - 1];
                var playerColor = playerMat.color;
                var playerColorEmission = playerMat.GetColor("_EmissionColor");
                // mat.shader.GetPropertyName(0);
                // print(mat.shader.GetPropertyName(0));
                mat.SetColor("_Color01", playerColor);
                mat.SetColor("_Color02", playerColor);
                mat.SetColor("_EmissionColor", playerColorEmission);
                trailRenderer.material = mat;
            }
        }
    }

    public void SetSpawn()
    {
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

    public void EnableTrail()
    {
        if (trailRenderer != null)
        {
            trailRenderer.emitting = true;
        }
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