﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Coroutine _spBlock;

    [SerializeField]
    private int[] monsterType = new int[6] { 10, 11, 20, 21, 30, 31 };
    private Vector3[] monsterSpawnPos = new Vector3[4];
    private Vector3 monsterSpawn;
    private float _spCoolDown = 0.0f;
    private float _spCoolTime = 2.0f;
    private Define.MonsterType monType;

    public void SpawnMonster(Define.MonsterType type)
    {
        GameObject player = GameObject.Find("Player");
        Vector3 playerPos = player.transform.position;

        monsterSpawnPos[0] = new Vector3(Random.Range(playerPos.x - 2.5f, playerPos.x - 2.0f), Random.Range(playerPos.y - 2.5f, playerPos.y + 2.5f), 0);
        monsterSpawnPos[1] = new Vector3(Random.Range(playerPos.x + 2.0f, playerPos.x + 2.5f), Random.Range(playerPos.y - 2.5f, playerPos.y + 2.5f), 0);
        monsterSpawnPos[2] = new Vector3(Random.Range(playerPos.x - 2.0f, playerPos.x + 2.0f), Random.Range(playerPos.y - 2.5f, playerPos.y - 2.0f), 0);
        monsterSpawnPos[3] = new Vector3(Random.Range(playerPos.x - 2.0f, playerPos.x + 2.0f), Random.Range(playerPos.y + 2.0f, playerPos.y + 2.5f), 0);

        monsterSpawn = monsterSpawnPos[Random.Range(0, 4)];
        if (_spBlock == null && _spCoolDown < 0)
        {
            _spBlock = StartCoroutine("SpBlock");
        }
    }
    public void SpawnDefender(Define.DefenderType type)
    {
        GameObject player = GameObject.Find("Player");
        Vector3 playerPos = player.transform.position;
        
        Vector3 spawnPos = new Vector3(Random.Range(playerPos.x - 0.5f, playerPos.x + 0.5f), Random.Range(playerPos.y - 0.5f, playerPos.y + 0.5f), 0);
        Managers.Defender.MakeDefender(type, spawnPos);
    }

    IEnumerator SpBlock()
    {
        Managers.Monster.MakeMonster(monType, monsterType[Random.Range(0, 6)], monsterSpawn);
        yield return new WaitForSeconds(0.1f);
        _spBlock = null;
        _spCoolDown = _spCoolTime;
    }
}
