using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    
    public PlayerCounter playerCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        playerCounter = GameObject.Find("PlayerCounter").GetComponent<PlayerCounter>();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("StartMenu"))
        {
            o.SetActive(false);
        }
    }
}
