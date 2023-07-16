using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private float _arrowSpeed = 7.0f;
    public int _attack = 10; // Arrow damage
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.Translate(Vector2.up*_arrowSpeed*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != tag)
        {
            Debug.Log(other.gameObject.name);
            
            Destroy(gameObject);
        }
            
    }
}
