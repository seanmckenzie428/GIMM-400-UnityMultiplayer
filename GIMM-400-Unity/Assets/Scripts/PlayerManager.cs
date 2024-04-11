using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.Serialization;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerCountUI;
    private PlayerInputManager _playerInputManager;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        _playerInputManager = GetComponent<PlayerInputManager>();
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        UpdatePlayerCountUI();
    }

    public void OnPlayerLeft(PlayerInput playerInput)
    {
        UpdatePlayerCountUI();
    }

    private void UpdatePlayerCountUI()
    {
        playerCountUI.text = "Players Joined: " + _playerInputManager.playerCount;
    }
}