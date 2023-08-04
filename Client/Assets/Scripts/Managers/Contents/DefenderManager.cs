using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderManager
{
    public void MakeDefender(Define.DefenderType type, Vector3 pos)
    {
        int defenderId = (int)type;
        string monsterName = "Defender" + "_" + defenderId.ToString("000");
        GameObject go = Managers.Resource.Instantiate($"Creature/Defender/{type.ToString()}/{monsterName}");
        go.name = "Defender"+type.ToString();
        pos.x = Mathf.Max(GameScene.LimitXDown, pos.x);
        pos.x = Mathf.Min(GameScene.LimitXUp, pos.x);
        pos.y = Mathf.Max(GameScene.LimitYDown, pos.y);
        pos.y = Mathf.Min(GameScene.LimitYUp, pos.y);
        go.transform.position = pos;
    }
}
