using System.Collections;
using System.Collections.Generic;
using Assets.PixelHeroes.Scripts.CharacterScrips;
using UnityEditor.SceneManagement;
using UnityEngine;
using AnimationState = Assets.PixelHeroes.Scripts.CharacterScrips.AnimationState;

public class CreatureController : MonoBehaviour
{
    public float _speed = 5.0f;
    public int _hp = 100;
    public int _attack = 10;
    public int _gold = 0;
    protected float _range = 1.0f;

    protected Rigidbody2D _rigidbody;
    protected Collider2D _collider;
    public Character _character;
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
        _hp -= damage;
        if (_hp <= 0)
        {
            _character.SetState(AnimationState.Dead);
            _collider.enabled = false;
            Destroy(gameObject,2f);
        }
    }
    public void SetState()
    {
        int _level = GameScene.StageLevel;
        _hp = 100+ 10*(_level);
        _attack = 10+10 * (_level/2);
        _gold = 10 * (_level);
    }
}
