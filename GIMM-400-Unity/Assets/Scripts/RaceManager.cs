
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaceManager : MonoBehaviour
{

    [SerializeField] private int raceLaps;
    [SerializeField] public Transform[] spawnPoints;
    [SerializeField] private LayerMask[] playerLayers;
    [SerializeField] private Camera defaultSceneCamera;

    private PlayerInputManager playerInputManager;
    private bool isStarted = false;
    private bool isRunning = false;
    private bool isFinished = false;
    private List<Racer> racers = new List<Racer>();

    public void Countdown()
    {
        
    }

    public void CheckForWin()
    {
        foreach (Racer racer in racers)
        {
            if (racer.lapsCompleted >= raceLaps)
            {
                WinGame(racer);
            }
        }
    }

    public void Start()
    {
        playerInputManager = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerInputManager>();
        playerInputManager.splitScreen = true;
        playerInputManager.DisableJoining();
        foreach (PlayerInput player in PlayerInput.all)
        {
            OnPlayerJoined(player);
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
            racer.raceManager = this;
            racer.SetPlayerColor();
            racer.SetSpawn();
            print("Racer: " + id + " joined");
            print(racer.transform);

            var racerParent = racer.transform.parent;

            int layerToAdd = (int)Mathf.Log(playerLayers[racers.Count - 1].value, 2);

            var racerVCam = racerParent.GetComponentInChildren<CinemachineVirtualCamera>();
            racerVCam.gameObject.layer = layerToAdd;
            racerVCam.Follow = racer.transform;
            racerVCam.LookAt = racer.transform;
            racerParent.GetComponentInChildren<Camera>().cullingMask = playerLayers[racers.Count - 1];

            if (defaultSceneCamera != null)
            {
                defaultSceneCamera.gameObject.SetActive(false);
            }
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
