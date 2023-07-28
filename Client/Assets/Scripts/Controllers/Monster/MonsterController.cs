using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Net.Sockets;
using Assets.PixelHeroes.Scripts.CollectionScripts;
using UnityEditor;
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
        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform,"MonsterHpBar");
    }

    protected override void Update()
    {
        if (_hp > 0 && _coAttack == null) Move();
        _rigidbody.velocity = Vector2.zero;
    }

    public override void OnDamaged(int damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            _character.SetState(AnimationState.Dead);
            _collider.enabled = false;
            CreatureController cc = GameScene.player.gameObject.GetComponent<CreatureController>();
            cc._gold += _gold;
            ++cc._killCount;
            Destroy(gameObject, 2f);
        }
    }
    protected override void Move()
    {
        if (_target != null)
        {
            Vector3 dir = _target.transform.position - transform.position;
            if (_target.transform.position.x < transform.position.x)
            {
                transform.Find("Body").transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
            else
            {
                transform.Find("Body").transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }

            if (dir.magnitude <= _attRange)
            {
                // TODO : �÷��̾� Attack �ϴ� �κ�, �÷��̾�� �Ÿ��� �����Ÿ� ���� ª�ٸ� ����
                if (_coAttack == null) _coAttack = StartCoroutine("CoStartAttack");
            }
            else _rigidbody.MovePosition(transform.position + dir.normalized * _speed * Time.deltaTime);
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
