using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _direction;
    [SerializeField] private float _thrust = 2f;

    private void OnEnable()
    {
        print("On enable was called");
    }

    private void OnDisable()
    {
        print("On disable was called");
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    public void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Activate(Vector2 ejectOrigin, Vector2 direction)
    {
        gameObject.SetActive(true);
        transform.position = ejectOrigin;
        _direction = direction;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name is "Boundary")
        {
            Deactivate();
        }
    }

    public void Move()
    {
        _rb.AddForce(_direction * _thrust);
    }
}
