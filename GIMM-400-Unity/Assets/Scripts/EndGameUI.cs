using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EndGameUI : MonoBehaviour
{
    public void Start()
    {
        var playAgainButton = GameObject.Find("PlayAgain");
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(playAgainButton);
    }

    public void RestartGame()
    {
        Destroy(GameObject.Find("PlayerManager"));
        SceneManager.LoadScene(0);
    }
}
