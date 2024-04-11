using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;

public class PlayerCounter : MonoBehaviour
{
    public int currentPlayerCount = 0;
    public bool playerCountLocked = false;
    public TextMeshProUGUI playerCountText;
    
    public List<InputDevice> playerDevices;
    

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    public void StartGame()
    {
        playerCountLocked = true;
        print("Starting game with " + currentPlayerCount + " players!");
    }
    
    public void OnPlayerJoined (PlayerInput playerInput)
    {
        if (playerCountLocked) return;
        currentPlayerCount++;
        print("Player joined! Current player count: " + currentPlayerCount);
        UpdateText();
        // playerDevices.Add(playerInput.devices[0]);
    }
    
    public void OnPlayerLeft (PlayerInput playerInput)
    {
        if (playerCountLocked) return;
        currentPlayerCount--;
        print("Player left! Current player count: " + currentPlayerCount);
        UpdateText();
        // playerDevices.Remove(playerInput.user.pairedDevices[0]);
    }

    private void UpdateText()
    {
        playerCountText.text = "Players Joined: " + currentPlayerCount;
    }
}
