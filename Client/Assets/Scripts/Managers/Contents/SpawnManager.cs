using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager
{
    
    public void SpawnDefender(Define.DefenderType type)
    {
        GameObject player = GameObject.Find("Player");
        Vector3 playerPos = player.transform.position;
        
        Vector3 spawnPos = new Vector3(Random.Range(playerPos.x - 0.5f, playerPos.x + 0.5f), Random.Range(playerPos.y - 0.5f, playerPos.y + 0.5f), 0);
        Managers.Defender.MakeDefender(type, spawnPos);
    }
}
