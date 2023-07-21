using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Net.Sockets;
using Assets.PixelHeroes.Scripts.CollectionScripts;
using UnityEngine;
using AnimationState = Assets.PixelHeroes.Scripts.CharacterScrips.AnimationState;

public class MonsterController : CreatureController
{
    [SerializeField]
    private GameObject _target;

    private Coroutine _coSearch;

    private int _layerMask;
    protected virtual void Start()
    {
        base.Start();
        _layerMask = (1 << LayerMask.NameToLayer("Player"));
        _coSearch = StartCoroutine("CoSearch");
        _speed = 2.0f;
    }

    protected override void Update()
    {
        if (_hp > 0 && _coAttack == null && _target) Move();
        _rigidbody.velocity = Vector2.zero;
    }

    protected virtual void Attack()
    {

    }

    protected override void Move()
    {
        if (_target != null)
        {
            Vector3 dir = _target.transform.position - transform.position;
            if (_target.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
            else
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }

            if (dir.magnitude <= _attRange)
            {
                // TODO : 플레이어 Attack 하는 부분, 플레이어와 거리가 사정거리 보다 짧다면 공격
                if (_coAttack == null) _coAttack = StartCoroutine("CoStartAttack");
            }
            _rigidbody.MovePosition(transform.position + dir.normalized * _speed * Time.deltaTime);
        }
    }
    protected Quaternion AttackAngle()
    {
        Vector2 dir = _target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle -90 , Vector3.forward);
    }
    IEnumerator CoSearch() 
    {
        while (true)
        {
            Collider2D col = Physics2D.OverlapCircle(transform.position, _range, _layerMask);
            if (col == null)
            {
                _target = BaseScene.player;
            }
            else
            {
                _target = col.gameObject;
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
    
}
