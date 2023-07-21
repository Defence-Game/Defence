using System.Collections;
using System.Collections.Generic;
using TMPro.SpriteAssetUtilities;
using UnityEngine;

public class KnightController : MonsterController
{
    protected override void Start()
    {
        base.Start();
        _attRange = (float)Define.MonsterAttRange.Knight;
        _range = _attRange*2;
    }

    protected override void Attack()
    {

    }

}
