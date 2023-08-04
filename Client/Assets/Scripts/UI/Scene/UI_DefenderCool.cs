using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_DefenderCool : UI_Scene
{
    public List<Image> skillFilter = new List<Image>();
    public Text _goldText; //남은 쿨타임을 표시할 텍스트
    public Button _btn;

    public float coolTime;
    private float currentCoolTime; //남은 쿨타임

    private bool canSummon = true;
    private PlayerController pc;
    private static bool _pause = false;

    public static bool StatePause { get { return _pause; } set { _pause = value; } }

    enum Buttons
    {
        UI_Knight,
        UI_Archer,
        UI_Mage,
        Pause
    }

    enum Images
    {
        Knight_fillter,
        Archer_fillter,
        Mage_fillter,
    }

    enum Texts
    {
        GoldText
    }

    public override void Init()
    {
        pc = BaseScene.player.GetComponent<PlayerController>();
        coolTime = pc._defenderCoolTime;

        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));

        _goldText = GetText((int)Texts.GoldText);

        skillFilter.Add(GetImage((int)Images.Knight_fillter));
        skillFilter.Add(GetImage((int)Images.Archer_fillter));
        skillFilter.Add(GetImage((int)Images.Mage_fillter));

        if (GetButton((int)Buttons.UI_Knight) != null)
        {
            GetButton((int)Buttons.UI_Knight).gameObject.BindEvent(SummonDefender);
        }
        if (GetButton((int)Buttons.UI_Archer) != null)
        {
            GetButton((int)Buttons.UI_Archer).gameObject.BindEvent(SummonDefender);
        }
        if (GetButton((int)Buttons.UI_Mage) != null)
        {
            GetButton((int)Buttons.UI_Mage).gameObject.BindEvent(SummonDefender);
        }
        if (GetButton((int)Buttons.Pause) != null)
        {
            GetButton((int)Buttons.Pause).gameObject.BindEvent(Pause);
        }
    }

    public void Update()
    {
        if (pc._gold >= pc._summonGold && pc._defenderCoolDown < 0)
        {
            if (Input.GetKey(KeyCode.Z))//Defender Knight summon
            {   
                SummonDefender("UI_Knight");
            }
            if (Input.GetKey(KeyCode.X))//Defender Archer summon
            {
                SummonDefender("UI_Archer");
            }
            if (Input.GetKey(KeyCode.C))//Defender Mage summon
            {
                SummonDefender("UI_Mage");
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        if(_goldText!=null)_goldText.text = $" : {pc._gold}";
    }
    private void Pause()
    {
        if (_pause == false)
        {
            Time.timeScale = 0;
            _pause = true;
            Managers.UI.ShowPopupUI<UI_Pause>();
        }
        else
        {
            Time.timeScale = 1;
            _pause = false;
            Managers.UI.ClosePopupUI();
        }
    }
    private void Pause(PointerEventData data)
    {
        if (_pause == false)
        {
            Time.timeScale = 0;
            _pause = true;
            Managers.UI.ShowPopupUI<UI_Pause>();
        }
        else
        {
            Time.timeScale = 1;
            _pause = false;
            Managers.UI.ClosePopupUI();
        }
    }
    private void SummonDefender(string name)
    {
        if (canSummon && pc._gold >= pc._summonGold)
        {
            GameObject player = GameObject.Find("Player");
            Vector3 playerPos = player.transform.position;
            Vector3 spawnPos = new Vector3(Random.Range(playerPos.x - 0.5f, playerPos.x + 0.5f), Random.Range(playerPos.y - 0.5f, playerPos.y + 0.5f), 0);
            
            switch (name)
            {
                case "UI_Archer":
                    Managers.Defender.MakeDefender(Define.DefenderType.Archer, spawnPos);
                    break;
                case "UI_Knight":
                    Managers.Defender.MakeDefender(Define.DefenderType.Knight, spawnPos);
                    break;
                case "UI_Mage":
                    Managers.Defender.MakeDefender(Define.DefenderType.Mage, spawnPos);
                    break;
            }

            for (int i = 0; i < skillFilter.Count; i++)
            {
                skillFilter[i].fillAmount = 1;
            }

            StartCoroutine("Cooltime");

            currentCoolTime = coolTime;

            StartCoroutine("CoolTimeCounter");
            pc._gold -= pc._summonGold;
            canSummon = false;
        }
        else
        {
            Debug.Log("소환이 불가능합니다.");
        }
    }
    private void SummonDefender(PointerEventData data)
    {
        string name = data.pointerClick.name;

        if (canSummon && pc._gold >= pc._summonGold)
        {
            GameObject player = GameObject.Find("Player");
            Vector3 playerPos = player.transform.position;
            Vector3 spawnPos = new Vector3(Random.Range(playerPos.x - 0.5f, playerPos.x + 0.5f), Random.Range(playerPos.y - 0.5f, playerPos.y + 0.5f), 0);

            switch (name)
            {
                case "UI_Archer":
                    Managers.Defender.MakeDefender(Define.DefenderType.Archer, spawnPos);
                    break;
                case "UI_Knight":
                    Managers.Defender.MakeDefender(Define.DefenderType.Knight, spawnPos);
                    break;
                case "UI_Mage":
                    Managers.Defender.MakeDefender(Define.DefenderType.Mage, spawnPos);
                    break;
            }

            for (int i = 0; i < skillFilter.Count; i++)
            {
                skillFilter[i].fillAmount = 1;
            }
            
            StartCoroutine("Cooltime");

            currentCoolTime = coolTime;
            
            StartCoroutine("CoolTimeCounter");
            pc._gold -= pc._summonGold;
            canSummon = false;
        }
        else
        {
            Debug.Log("소환이 불가능합니다.");
        }
    }
    IEnumerator Cooltime()
    {
        while (skillFilter[0].fillAmount > 0)
        {
            for (int i = 0; i < skillFilter.Count; i++)
            {
                skillFilter[i].fillAmount -= 1 * Time.smoothDeltaTime / coolTime;
            }
            yield return null;
        }

        canSummon = true;

        yield break;
    }

    IEnumerator CoolTimeCounter()
    {
        while (currentCoolTime > 0)
        {
            yield return new WaitForSeconds(1.0f);

            currentCoolTime -= 1.0f;
        }

        yield break;
    }
}
