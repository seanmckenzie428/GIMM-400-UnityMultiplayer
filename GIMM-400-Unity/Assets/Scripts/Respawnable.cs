using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawnable : MonoBehaviour
{
    private Transform lastRespawnPoint;

    public void Respawn()
    {
        transform.position = lastRespawnPoint.position;
        transform.rotation = lastRespawnPoint.rotation;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
