using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float _speed = 5.0f;
    public static int _attack = (GameScene.StageLevel/2)*10+ 10;
    public int Attack { get { return (GameScene.StageLevel / 2) * 10 + 10;}}
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.Translate(Vector2.up* _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != tag)
        {
            Debug.Log(other.gameObject.name);
            CreatureController cc = other.gameObject.GetComponent<CreatureController>();
            cc.OnDamaged(Attack);
            Destroy(gameObject);
        }
    }
}
