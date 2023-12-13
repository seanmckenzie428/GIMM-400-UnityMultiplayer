using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{

    public Transform[] spawnPoints;
    public Transform[] respawnPoints;

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        playerInput.GetComponent<Bike_Sphere>().spawnPoints = spawnPoints;
        playerInput.GetComponent<Bike_Sphere>().respawnPoints = respawnPoints;
        print("Hello there");
    }
}
