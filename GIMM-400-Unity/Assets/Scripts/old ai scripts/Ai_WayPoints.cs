using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai_WayPoints : MonoBehaviour
{
  public Transform[] waypoints;

    // Optionally, you can use Gizmos to visualize the waypoints in the Scene view
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if (waypoints != null)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                Gizmos.DrawSphere(waypoints[i].position, 0.5f);
            }
        }
    }
}

