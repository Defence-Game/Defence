using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    }

    void Resume(PointerEventData data)
    {
        Time.timeScale = 1;
        UI_DefenderCool.StatePause = !UI_DefenderCool.StatePause;
        ClosePopupUI();
    }

}
