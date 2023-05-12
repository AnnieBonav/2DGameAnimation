using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    public static event Action<float> HitPlayer;
    private Rigidbody2D _rb;
    private Vector2 _direction;
    [SerializeField] private float _thrust = 2f;
    [SerializeField] private Transform _target;
    [SerializeField] private float _damage = 5;

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
        float degrees = Vector2.SignedAngle(new Vector2(1,0), direction);
        transform.rotation = Quaternion.Euler(0, 0, degrees);
        _direction = direction;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        HandleMove();
        //HandleRotation();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name is "Boundary")
        {
            Deactivate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")) {
            HandleHitPlayer();
        }else if (collider.CompareTag("World"))
        {
            Deactivate();
        }
    }

    private void HandleHitPlayer()
    {
        HitPlayer?.Invoke(_damage);
    }

    public void HandleMove()
    {
        //_rb.AddForce(_direction * _thrust);
        transform.position = Vector2.MoveTowards(transform.position, _target.position, _thrust);
    }

    public void HandleRotation()
    {
        transform.up = _target.position - transform.position;
    }
}
