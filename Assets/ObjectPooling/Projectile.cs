using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _velocity;
    private float _thrust = 1f;

    private void OnEnable()
    {
        print("On enable was called");
    }

    private void OnDisable()
    {
        print("On disable was called");
    }

    public void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Activate(Vector2 ejectOrigin)
    {
        //transform.position = ejectOrigin;
        OnEnable();
    }

    public void Deactivate()
    {
        OnDisable();
    }

    private void FixedUpdate()
    {

        Move();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        print("Entered. Collision: " + collision.name + " Velocity: " + _velocity);
        if(collision.name is "Boundary")
        {
            gameObject.SetActive(false);
        }
    }

    public void Move()
    {
        //_rb.AddForce(new Vector2(_velocity, 0)); // TODO: Get direction based on ejection. Laundher sends it to arrow instance
        _rb.AddForce(new Vector2(_velocity, _velocity));
        //print("Move: " + _velocity);
    }
}
