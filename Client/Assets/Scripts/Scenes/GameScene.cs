﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameScene : BaseScene
{
    public static int StageLevel { get; set; } = 1;
    private CreatureController cc;
    
    // monster
    [SerializeField]
    public static int _monsterNum = 0;
    private int[] monsterType = new int[6] { 10, 11, 20, 21, 30, 31 };
    private int monsterInt;
    private Vector3[] monsterSpawn = new Vector3[4];
    private Vector3 monsterSpawnPos;
    private float _spCoolDown = 0.0f;
    private float _spCoolTime = 5.0f;
    
    private Define.MonsterType monType;
    
    public static int LimitXUp = 29;
    public static int LimitXDown = -26;
    public static int LimitYUp = 16;
    public static int LimitYDown = -16;
    
    private Coroutine _spBlock;
    
    protected override void Init()
    {
        base.Init();

        StageLevel = 1;
        SceneType = Define.Scene.Game;
        Managers.Map.LoadMap(1);
        player = Managers.Resource.Instantiate("Creature/Player");
        player.name = "Player";
        cc = player.GetComponent<CreatureController>();
        
        _spBlock = StartCoroutine("SpBlock");

        Managers.UI.ShowSceneUI<UI_DefenderCool>();
    }

    private void Update()
    {
        if (cc._killCount >10)
        {
            cc._killCount = 0;
            ++StageLevel;
            cc._attack += 10;
            cc._maxHp += 10;
            cc._hp += 10;
        }
    }
    IEnumerator SpBlock()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            for (int i = 0; i < 7; i++)
            {
                if(_monsterNum>14)continue;
                MonsterRandom();
                Managers.Monster.MakeMonster(monType, monsterType[monsterInt], monsterSpawnPos);
                _monsterNum++;
            }
            yield return new WaitForSeconds(8.0f);
        }
        
    }
    
    void MonsterRandom()
    {
        if (player == null) return;
        Vector3 playerPos = player.transform.position;
        monsterSpawn[0] = new Vector3(Random.Range(playerPos.x - 2.5f, playerPos.x - 2.0f), Random.Range(playerPos.y - 2.5f, playerPos.y + 2.5f), 0);
        monsterSpawn[1] = new Vector3(Random.Range(playerPos.x + 2.0f, playerPos.x + 2.5f), Random.Range(playerPos.y - 2.5f, playerPos.y + 2.5f), 0);
        monsterSpawn[2] = new Vector3(Random.Range(playerPos.x - 2.0f, playerPos.x + 2.0f), Random.Range(playerPos.y - 2.5f, playerPos.y - 2.0f), 0);
        monsterSpawn[3] = new Vector3(Random.Range(playerPos.x - 2.0f, playerPos.x + 2.0f), Random.Range(playerPos.y + 2.0f, playerPos.y + 2.5f), 0);
        monsterInt = Random.Range(0, 6);
        monsterSpawnPos = monsterSpawn[Random.Range(0, 4)];

        monsterSpawnPos.x = Mathf.Max(LimitXDown, monsterSpawnPos.x);
        monsterSpawnPos.x = Mathf.Min(LimitXUp, monsterSpawnPos.x);
        monsterSpawnPos.y = Mathf.Max(LimitYDown, monsterSpawnPos.y);
        monsterSpawnPos.y = Mathf.Min(LimitYUp, monsterSpawnPos.y);

        if (monsterType[monsterInt] < 20)
        {
            monType = Define.MonsterType.Elf;
        }
        else if (monsterType[monsterInt] < 30)
        {
            monType = Define.MonsterType.Goblin;
        }
        else
        {
            monType = Define.MonsterType.Skeleton;
        }
    }
    
    public override void Clear()
    {
        _monsterNum = 0;
    }
}
