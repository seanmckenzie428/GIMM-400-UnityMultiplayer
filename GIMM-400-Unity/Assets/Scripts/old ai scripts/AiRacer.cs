using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiRacer : MonoBehaviour
{
     public Transform[] waypoints;
    public float moveSpeed = 5f;
    public float rotationSpeed = 2f;

    private int currentWaypointIndex = 0;

    void Update()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            Vector3 directionToWaypoint = waypoints[currentWaypointIndex].position - transform.position;
            directionToWaypoint.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(directionToWaypoint);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 1f)
            {
                currentWaypointIndex++;
            }
        }
    }
}
