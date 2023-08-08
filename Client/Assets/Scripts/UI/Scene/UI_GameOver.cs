using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameOver : UI_Scene
{
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("Lobby");
            Time.timeScale = 1;
            UI_DefenderCool.StatePause = false;
        }
    }
}
