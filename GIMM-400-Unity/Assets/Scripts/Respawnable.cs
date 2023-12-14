using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawnable : MonoBehaviour
{
    private Transform lastRespawnPoint;
    private Rigidbody rb;

    public void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void Respawn()
    {
        if (lastRespawnPoint == null)
        {
            Debug.LogError("No respawn point set for " + gameObject.name);
            return;
        }
        transform.position = lastRespawnPoint.position;
        transform.rotation = lastRespawnPoint.rotation;
        rb.velocity = Vector3.zero;
    }

    public void SetRespawnPoint(Transform newRespawnPoint)
    {
        lastRespawnPoint = newRespawnPoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RespawnPoint"))
        {
            SetRespawnPoint(other.transform);
        }
        else if (other.CompareTag("Respawn"))
        {
            Respawn();
        }
    }
}
