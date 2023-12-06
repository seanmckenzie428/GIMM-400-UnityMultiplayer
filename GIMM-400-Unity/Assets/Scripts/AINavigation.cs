using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINavigation : MonoBehaviour
{
     public Transform[] waypoints;  // Array of waypoints representing the racing course.
    public float lapDistanceThreshold = 1.0f;  // Threshold to determine if the AI has completed a lap.
    public int maxLaps = 3;  // Maximum number of laps to complete.

    private UnityEngine.AI.NavMeshAgent agent;
    private int currentWaypointIndex = 0;
    private int currentLap = 0;
    private float lapStartTime;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        StartNewLap();
    }

    void Update()
    {
        if (currentLap < maxLaps)
        {
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < lapDistanceThreshold)
            {
                if (currentWaypointIndex < waypoints.Length - 1)
                {
                    currentWaypointIndex++;
                }
                else
                {
                    StartNewLap();
                }
            }

            NavigateToWaypoint();
        }
    }

    void NavigateToWaypoint()
    {
        if (waypoints.Length > 0 && agent != null)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    void StartNewLap()
    {
        currentWaypointIndex = 0;
        currentLap++;
        lapStartTime = Time.time;
        Debug.Log("Lap " + currentLap + " started!");
    }
}