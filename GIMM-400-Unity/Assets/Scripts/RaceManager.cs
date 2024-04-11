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
    private List<Player> players = new List<Player>();
    private int countdownValue = 3;

    public void Start()
    {
        playerInputManager = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerInputManager>();
        playerInputManager.splitScreen = true;
        playerInputManager.DisableJoining();
        foreach (PlayerInput player in PlayerInput.all)
        {
            OnPlayerJoined(player);
        }
        
        StartCountdown();
    }
    
    public void StartCountdown()
    {
        InvokeRepeating(nameof(Countdown), 0, 1);
    }
    
    public void Countdown()
    {
        if (countdownValue > 0)
        {
            SetCountdownForPlayers(countdownValue.ToString());
        }
        else if (countdownValue == 0)
        {
            SetCountdownForPlayers("GO!");
            StartRace();
        } else if (countdownValue < -1) // Show GO! for 2 seconds
        {
            SetCountdownForPlayers("");
            CancelInvoke(nameof(Countdown));
        }
        
        countdownValue--;
    }
    
    private void SetCountdownForPlayers(string value = "")
    {
        foreach (var player in players)
        {
            player.SetCountDownText(value);
        }
    }
    
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        var racer = playerInput.gameObject.GetComponent<Racer>();
        if (racer)
        {
            racers.Add(racer);
            if (racer.player != null)
            {
                players.Add(racer.player);
            }
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
    
    public void StartRace()
    {
        isStarted = true;
        isRunning = true;

        foreach (var racer in racers)
        {
            racer.rb.isKinematic = false;
        }
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