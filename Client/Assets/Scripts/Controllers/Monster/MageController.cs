using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageController : MonsterController
{
    protected override void Start()
    {
        base.Start();
        _attRange = (float)Define.MonsterAttRange.Mage;
        _range = _attRange * 2;
    }

    protected override void Attack()
    {

    }
}
