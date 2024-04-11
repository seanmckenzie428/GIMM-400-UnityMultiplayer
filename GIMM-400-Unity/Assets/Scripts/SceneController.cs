using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public void LoadGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        HideStartMenu();
    }

    private static void HideStartMenu()
    {
        foreach (var o in GameObject.FindGameObjectsWithTag("StartMenu"))
        {
            o.SetActive(false);
        }
    }
}
