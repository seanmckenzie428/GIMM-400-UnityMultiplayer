using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINavigation : MonoBehaviour
{
     public Transform[] waypoints;
    public float lapDistanceThreshold = 1.0f;
    public int maxLaps = 3;

    private UnityEngine.AI.NavMeshAgent agent;
    private int currentWaypointIndex = 0;
    private int currentLap = 0;
    private float lapStartTime;
    private bool aiFinishedRace = false;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        StartNewLap();
    }

    void Update()
    {
        if (!aiFinishedRace)
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
            else if (currentLap >= maxLaps && !aiFinishedRace)
            {
                aiFinishedRace = true;
                HandleRaceFinish(true);
            }
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

    void HandleRaceFinish(bool isAI)
    {
        if (isAI)
        {
            Debug.Log("AI Wins!");
            // You can add any additional actions for AI victory here.
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Finish"))
        {
            // You can add any lap-related logic here.
        }

        if (other.CompareTag("Respawn"))
        {
            // You can add any respawn-related logic here.
        }
    }
}