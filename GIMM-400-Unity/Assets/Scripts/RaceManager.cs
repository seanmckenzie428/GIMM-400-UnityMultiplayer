
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaceManager : MonoBehaviour
{

    [SerializeField]
    private int raceLaps;

    [SerializeField] public Transform[] spawnPoints;
    [SerializeField] private LayerMask[] playerLayers;
    
    private bool isStarted = false;
    private bool isRunning = false;
    private bool isFinished = false;
    private List<Racer> racers = new List<Racer>();

    private void CheckForWin()
    {
        foreach (Racer racer in racers)
        {
            if (racer.lapsCompleted >= raceLaps)
            {
                WinGame(racer);
            }
        }
    }


    public void OnPlayerJoined(PlayerInput playerInput)
    {
        var racer = playerInput.gameObject.GetComponent<Racer>();
        if (racer)
        {
            racers.Add(racer);
            var id = racers.Count;
            racer.id = id;
            print("Racer: " + id + " joined");
            print(racer.transform);

            // var racerParent = racer.transform.parent;

            // int layerToAdd = (int)Mathf.Log(playerLayers[racers.Count - 1].value, 2);

            // var racerVCam = racerParent.GetComponentInChildren<CinemachineVirtualCamera>();
            // racerVCam.gameObject.layer = layerToAdd;
            // racerVCam.Follow = racer.transform;
            // racerVCam.LookAt = racer.transform;
            // racerParent.GetComponentInChildren<Camera>().cullingMask = playerLayers[racers.Count - 1];
        }
    }
    
    public void OnPlayerLeft(PlayerInput playerInput)
    {
        var racer = playerInput.gameObject.GetComponent<Racer>();
        if (racer)
        {
            racers.Remove(racer);
            print("Racer: " + racer.id + " left");
        }
    }

    public void WinGame(Racer winner)
    {
        Debug.Log("Player " + winner.id + " won!!!");
    }
}
