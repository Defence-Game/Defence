using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderMage : DefenderController
{
    protected override void Start()
    {
        base.Start();
        _attRange = (float)Define.AttRange.Mage;
        _range = _attRange * 2;
    }

    IEnumerator CoStartAttack()
    {
        _character.Animator.SetTrigger("Attack");
        GameObject ball = Managers.Resource.Instantiate("Creature/Fireballs/BallTailBlue");
        ball.tag = "Player";
        ball.transform.rotation = AttackAngle();
        ball.transform.position = transform.position;

        Destroy(ball, _lifeTime);
        yield return new WaitForSeconds(1.0f);
        _coAttack = null;
    }
}
