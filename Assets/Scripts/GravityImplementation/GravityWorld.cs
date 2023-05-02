using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWorld : MonoBehaviour
{
    [SerializeField] private GameObject _world;
    private Rigidbody2D _rb;
    [SerializeField] private float _gravityForce;
    [SerializeField] private float _gravityDistance;
    private float _lookAngle;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(gameObject.transform.position, _world.transform.position);
        Vector3 v = _world.transform.position - transform.position;
        _rb.AddForce(v.normalized * (1.0f - distance / _gravityDistance) * _gravityForce);
        _lookAngle = 90 + Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, _lookAngle);
    }
}
