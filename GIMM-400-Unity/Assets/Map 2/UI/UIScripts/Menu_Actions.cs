using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Actions : MonoBehaviour
{
    public static bool GameIsPaused = false;

    void Resume()
    {

    }
    void Pause ()
    {
        SceneManager.LoadScene("Pause_Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
