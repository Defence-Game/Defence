using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderManager
{
    public void MakeDefender(Define.DefenderType type)
    {
        int defenderId = (int)type;
        string monsterName = "Defender" + "_" + defenderId.ToString("000");
        GameObject go = Managers.Resource.Instantiate($"Creature/Defender/{type.ToString()}/{monsterName}");
        go.name = "Defender"+type.ToString();
    }
}
