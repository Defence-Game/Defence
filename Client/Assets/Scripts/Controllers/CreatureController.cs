using System.Collections;
using System.Collections.Generic;
using Assets.PixelHeroes.Scripts.CharacterScrips;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;
using AnimationState = Assets.PixelHeroes.Scripts.CharacterScrips.AnimationState;

public class CreatureController : MonoBehaviour
{
    public float _speed = 5.0f;
    public int _maxHp = 100;
    public int _hp = 100;
    public int _attack = 10;
    public int _gold = 0;
    protected float _range = 1.0f;
    protected float _attRange;
    protected float _lifeTime = 3.0f; // ����ü ���� �ð�
    public int _killCount=0;

    protected Rigidbody2D _rigidbody;
    protected Collider2D _collider;
    public Character _character;
    public Slider _hpBar;
    
    protected Coroutine _coAttack;
    protected Coroutine _coOnDamaged;

    protected virtual void Start()
    {
        _character.SetState(AnimationState.Idle);
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        SetState();
    }

    protected virtual void Update()
    {
        if(_hp>0)Move();
        _rigidbody.velocity = Vector2.zero;
    }

    protected virtual void Move()
    {
        
    }

    public virtual void OnDamaged(int damage)
    {
    }
    public void SetState()
    {
        int _level = GameScene.StageLevel;
        _maxHp = 100+ 10*(_level);
        _hp = _maxHp;
        _attack = 10+10 * (_level/2);
        _gold = 10 * (_level);
    }

}
