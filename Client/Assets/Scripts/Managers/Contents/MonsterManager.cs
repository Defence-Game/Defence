using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager
{
    public void MakeMonster(Define.MonsterType type, int monsterId)
    {
        string monsterName = type.ToString() + "_" + monsterId.ToString("000");
        GameObject go = Managers.Resource.Instantiate($"Creature/Monster/{type.ToString()}/{monsterName}");
        go.name = "Monster"+type.ToString();
    }

}
