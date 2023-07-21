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
        base.Update();
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

            if (dir.magnitude <= _range)
            {
                // TODO : �÷��̾� Attack �ϴ� �κ�, �÷��̾�� �Ÿ��� �����Ÿ� ���� ª�ٸ� ����
            }
            _rigidbody.MovePosition(transform.position + dir.normalized * _speed * Time.deltaTime);
        }
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