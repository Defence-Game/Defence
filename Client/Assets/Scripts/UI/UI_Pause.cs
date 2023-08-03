using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Pause : UI_Popup
{
    enum Buttons
    {
        ResumeBtn,
        ExitBtn
    }

    public void Start()
    {
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.ResumeBtn).gameObject.BindEvent(Resume);
        GetButton((int)Buttons.ExitBtn).gameObject.BindEvent(ExitGame);
    }

    void Resume(PointerEventData data)
    {
        Time.timeScale = 1;
        UI_DefenderCool.StatePause = !UI_DefenderCool.StatePause;
        ClosePopupUI();
    }

    void ExitGame(PointerEventData data)
    {
        Time.timeScale = 1;
        UI_DefenderCool.StatePause = !UI_DefenderCool.StatePause;
        SceneManager.LoadScene("Lobby");
    }

}
