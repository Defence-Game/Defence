using System.Collections;
using System.Collections.Generic;
using Assets.PixelHeroes.Scripts.CharacterScrips;
using UnityEngine;
using AnimationState = Assets.PixelHeroes.Scripts.CharacterScrips.AnimationState;

public class CreatureController : MonoBehaviour
{
    public float _speed = 1.5f;
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
}
