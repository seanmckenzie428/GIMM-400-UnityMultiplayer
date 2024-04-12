using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.Serialization;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject[] playerPrefabs;
    [SerializeField] private TextMeshProUGUI playerCountUI;
    private PlayerInputManager _playerInputManager;
    private int playerPrefabIndex = 0;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        _playerInputManager = GetComponent<PlayerInputManager>();
        _playerInputManager.playerPrefab = playerPrefabs[playerPrefabIndex];
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        UpdatePlayerCountUI();
        playerPrefabIndex++;
        _playerInputManager.playerPrefab = playerPrefabs[playerPrefabIndex];
    }

    public void OnPlayerLeft(PlayerInput playerInput)
    {
        UpdatePlayerCountUI();
        playerPrefabIndex--;
        _playerInputManager.playerPrefab = playerPrefabs[playerPrefabIndex];
    }

    private void UpdatePlayerCountUI()
    {
        playerCountUI.text = "Players Joined: " + _playerInputManager.playerCount;
    }
}