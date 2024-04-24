using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveTest : MonoBehaviour
{
    private Rigidbody _rb;
    
    [SerializeField] private float xVelocity;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = Vector3.forward * xVelocity;
    }
}
