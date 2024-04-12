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
        print("Respawning...");
        print(lastRespawnPoint);
        if (lastRespawnPoint == null)
        {
            Debug.LogWarning("No respawn point set for " + gameObject.name);
            var racer = gameObject.GetComponent<Racer>();
            racer.SetSpawn();
            return;
        }
        transform.position = lastRespawnPoint.position;

        var rot = lastRespawnPoint.rotation.eulerAngles;
        // rotate 90 degrees on y axis
        rot.y -= 90;
        transform.rotation = Quaternion.Euler(rot);
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
