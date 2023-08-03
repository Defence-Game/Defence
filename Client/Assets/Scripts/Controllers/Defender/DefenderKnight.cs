using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderKnight : DefenderController
{
    protected override void Start()
    {
        base.Start();
        _attRange = (float)Define.AttRange.Knight / 30;
        _range = _attRange * 2;
    }

    IEnumerator CoStartAttack()
    {
        _character.Animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1.0f);
        _coAttack = null;
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag != tag && _coAttack == null)
        {
            _coAttack = StartCoroutine("CoStartAttack");
            CreatureController cc = other.gameObject.GetComponent<CreatureController>();
            cc.OnDamaged(_attack);
        }
    }
}
