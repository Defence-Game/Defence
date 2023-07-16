using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using AnimationState = Assets.PixelHeroes.Scripts.CharacterScrips.AnimationState;

public class PlayerController : CreatureController
{
    private Coroutine _coAttack;
    private Rigidbody2D _rigidbody;

    private float arrowLifeTime = 3.0f;
    protected override void Start()
    {
        _character.SetState(AnimationState.Idle);
        _rigidbody = GetComponent<Rigidbody2D>();
        _coAttack = StartCoroutine("CoStartAttack");
    }

    protected override void Update()
    {
        base.Update();
        _rigidbody.velocity = Vector3.zero;
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
        if (movement == Vector3.zero)
        {
            _character.SetState(AnimationState.Idle);
        }
        else
        {
            if (movement.x < 0) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            else transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            _character.SetState(AnimationState.Walking);
            _rigidbody.MovePosition(transform.position + movement * Time.deltaTime * _speed);
            //transform.position += movement * Time.deltaTime * _speed;
        }
    }

    public Quaternion ArrowShootAngle()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,Camera.main.transform.position.z)) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle+90, Vector3.forward);
    }

    IEnumerator CoStartAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            _character.Animator.SetTrigger("Attack");
            GameObject arrow = Managers.Resource.Instantiate("Creature/Arrow");
            arrow.tag = "Player";
            arrow.transform.rotation = ArrowShootAngle();
            arrow.transform.position = transform.position;
            Destroy(arrow,arrowLifeTime);
        }
    }
}
