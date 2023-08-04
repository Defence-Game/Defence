using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Lobby : UI_Scene
{
    enum Buttons
    {
        StartBtn,
        ExitBtn
    }
    public override void Init()
    {
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.StartBtn).gameObject.BindEvent(StartGame);
        GetButton((int)Buttons.ExitBtn).gameObject.BindEvent(ExitGame);
    }

    private void StartGame(PointerEventData data)
    {
        SceneManager.LoadScene("Game");
    }
    private void ExitGame(PointerEventData data)
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
