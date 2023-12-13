
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaceManager : MonoBehaviour
{

    [SerializeField]
    private int raceLaps;

    [SerializeField] private Transform[] spawnPoints;
    
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
            racer.SetSpawn(spawnPoints[id-1]);
        }
    }

    public void WinGame(Racer winner)
    {
        Debug.Log("Player " + winner.id + " won!!!");
    }
}
