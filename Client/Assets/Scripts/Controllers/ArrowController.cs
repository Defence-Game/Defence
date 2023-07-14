using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private float _arrowSpeed = 3.0f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector2.up*_arrowSpeed*Time.deltaTime);
    }
}
