using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Actions : MonoBehaviour
{
    public static bool GameIsPaused = false;

    // Update is called once per frame
    void Update()
    {
     if(input.GetKeyDown(KeyCode.Escape))
     {
        if(GameIsPaused)
        {
            Resume();
        } else
        {
            Pause();
        }
     }   
    }

    void Resume()
    {

    }
    void Pause ()
    {
        SceneManager.LoadScene(Pause_Menu);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
