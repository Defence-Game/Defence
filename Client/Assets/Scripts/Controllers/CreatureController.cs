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

    void Start()
    {
        _character.SetState(AnimationState.Idle);
    }

    void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        Vector3 movement = new Vector2();
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        if (movement == Vector3.zero)
        {
            _character.SetState(AnimationState.Idle);
        }
        else
        {
            if (movement.x < 0) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            else transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            _character.SetState(AnimationState.Walking);
            transform.position += movement * Time.deltaTime * _speed;
        }
    }
}
