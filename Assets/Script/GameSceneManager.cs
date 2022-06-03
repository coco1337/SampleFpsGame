using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This scripts contains scene context and manage game state
public class GameSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
    }

    public void ResumeGame() 
    {
        Time.timeScale = 1.0f;
    }
}
