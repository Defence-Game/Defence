using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : MonsterController
{
    protected override void Start()
    {
        base.Start();
        _attRange = (float)Define.MonsterAttRange.Archer;
        _range = _attRange * 2;
    }

    protected override void Attack()
    {

    }
    IEnumerator CoStartAttack()
    {
        _character.Animator.SetTrigger("Attack");
        GameObject arrow = Managers.Resource.Instantiate("Creature/Arrow");
        arrow.tag = "Monster";
        arrow.transform.rotation = AttackAngle();
        arrow.transform.position = transform.position;
        Destroy(arrow, _lifeTime);
        yield return new WaitForSeconds(1.0f);
        _coAttack = null;
    }
}
