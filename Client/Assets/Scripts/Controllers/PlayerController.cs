using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using AnimationState = Assets.PixelHeroes.Scripts.CharacterScrips.AnimationState;

public class PlayerController : CreatureController
{
    private Coroutine _coBlock;
    private Coroutine _deBlock;
    [SerializeField]
    private float _coolDown=0.0f;
    private float _coolTime = 10.0f;
    private float _defenderCoolTime = 10.0f;
    private float _defenderCoolDown = 0.0f;
    private SpriteRenderer _sprite;
    [SerializeField]
    private bool _isInvincible = false;
    private bool _isAlive = true;
    
    protected override void Start()
    {
        base.Start();
        _coAttack = StartCoroutine("CoStartAttack");
        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform,"DefenderHpBar");
    }

    protected override void Update()
    {
        base.Update();
        _coolDown -= Time.deltaTime;
        _defenderCoolDown -= Time.deltaTime;
    }

    void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -2.5f);
    }
 
    protected override void Move()
    {
        Vector3 movement = new Vector3();
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        if (movement == Vector3.zero && _character.GetState() != AnimationState.Blocking)
        {
            _character.SetState(AnimationState.Idle);
        }
        else
        {
            if (movement.x < 0) transform.Find("Body").transform.localScale= new Vector3(-1.0f, 1.0f, 1.0f);
            else transform.Find("Body").transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            _character.SetState(AnimationState.Walking);
            _rigidbody.MovePosition(transform.position + movement * Time.deltaTime * _speed);
        }

        if (Input.GetKey(KeyCode.Space) && _coBlock==null && _coolDown<0)
        {
            _coBlock = StartCoroutine("CoBlock");
        }
        if (Input.GetKey(KeyCode.Z) && _deBlock == null && _defenderCoolDown < 0)//Defender Knight summon
        {
            _deBlock = StartCoroutine("DeBlockKnight");
        }
        if (Input.GetKey(KeyCode.X) && _deBlock == null && _defenderCoolDown < 0)//Defender Archer summon
        {
            _deBlock = StartCoroutine("DeBlockArcher");
        }
        if (Input.GetKey(KeyCode.C) && _deBlock == null && _defenderCoolDown < 0)//Defender Mage summon
        {
            _deBlock = StartCoroutine("DeBlockMage");
        }
    }

    public override void OnDamaged(int damage)
    {
        if (_isInvincible) return;
        _hp -= damage;
        if (_hp <= 0)
        {
            _character.SetState(AnimationState.Dead);
            _isAlive = false;
            _collider.enabled = false;
            Destroy(gameObject);
        }
    }
    protected Quaternion AttackAngle()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z)) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }
    IEnumerator CoStartAttack()
    {
        while (_isAlive)
        {
            yield return new WaitForSeconds(0.3f);
            _character.Animator.SetTrigger("Attack");
            GameObject arrow = Managers.Resource.Instantiate("Creature/Arrow");
            arrow.tag = "Player";
            arrow.transform.rotation = AttackAngle();
            arrow.transform.position = transform.position;
            Destroy(arrow,_lifeTime);
        }
    }

    IEnumerator CoBlock()
    {
        _isInvincible = true;
        for (int i = 0; i < 5; ++i)
        {
            _character.Blink();
            yield return new WaitForSeconds(0.2f);
        }
        _isInvincible = false;
        _coBlock = null;
        _coolDown = _coolTime;
    }

    IEnumerator DeBlockKnight()
    {
        Managers.Spawn.SpawnDefender(Define.DefenderType.Knight);
        yield return new WaitForSeconds(0.1f);
        _deBlock = null;
        _defenderCoolDown = _defenderCoolTime;
    }
    IEnumerator DeBlockArcher()
    {
        Managers.Spawn.SpawnDefender(Define.DefenderType.Archer);
        yield return new WaitForSeconds(0.1f);
        _deBlock = null;
        _defenderCoolDown = _defenderCoolTime;
    }
    IEnumerator DeBlockMage()
    {
        Managers.Spawn.SpawnDefender(Define.DefenderType.Mage);
        yield return new WaitForSeconds(0.1f);
        _deBlock = null;
        _defenderCoolDown = _defenderCoolTime;
    }
}
