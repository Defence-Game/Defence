using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameScene : BaseScene
{
    public static int StageLevel { get; set; } = 1;
    private CreatureController cc;
    
    // monster
    private int[] monsterType = new int[6] { 10, 11, 20, 21, 30, 31 };
    private int monsterInt;
    private Vector3[] monsterSpawn = new Vector3[4];
    private Vector3 monsterSpawnPos;
    private float _spCoolDown = 0.0f;
    private float _spCoolTime = 5.0f;
    private Define.MonsterType monType;
    
    private Coroutine _spBlock;
    
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
        Managers.Map.LoadMap(1);
        
        player = Managers.Resource.Instantiate("Creature/Player");
        player.name = "Player";
        cc = player.GetComponent<CreatureController>();
        
        _spBlock = StartCoroutine("SpBlock");
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
                MonsterRandom();
                Managers.Monster.MakeMonster(monType, monsterType[monsterInt], monsterSpawnPos);
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
        
    }
}
