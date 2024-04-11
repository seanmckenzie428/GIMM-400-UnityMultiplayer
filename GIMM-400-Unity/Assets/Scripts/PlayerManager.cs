using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.Serialization;

public class PlayerManager : MonoBehaviour
{
    private PlayerCounter playerCounter;

    [SerializeField] private GameObject[] players;
    
    private PlayerInputManager playerInputManager;
    
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        playerInputManager = GetComponent<PlayerInputManager>();
        playerCounter = GameObject.Find("PlayerCounter").GetComponent<PlayerCounter>();
        // JoinPlayers();
        // EnablePlayers();
    }
    
    public void OnPlayerJoined (PlayerInput playerInput)
    {
        playerCounter.OnPlayerJoined(playerInput);
    }

    // private void JoinPlayers()
    // {
    //     for (int i = 0; i < playerCounter.currentPlayerCount; i++)
    //     {
    //         playerInputManager.JoinPlayer(-1, -1, null, playerCounter.playerDevices[i]);
    //     }
    // }

    private void EnablePlayers()
    {
        for (int i = 0; i < playerCounter.currentPlayerCount; i++)
        {
            players[i].SetActive(true);
        }
    }
}