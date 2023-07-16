using System.Collections;
using System.Collections.Generic;
using Assets.PixelHeroes.Scripts.CharacterScrips;
using UnityEngine;
using AnimationState = Assets.PixelHeroes.Scripts.CharacterScrips.AnimationState;

public class CreatureController : MonoBehaviour
{
    public float _speed = 5.0f;
    public int _hp = 100;
    public int _attack = 10;
    public int _gold = 0;

    public Character _character;
    public CharacterController Controller;

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
    }

    public virtual void OnDamaged()
    {

    }


}
