using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{
    [SerializeField] float _movingSpeed = 0.1f;
    [SerializeField] float _travelHorizontal = 20;
    Transform _startTransform;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _startTransform = transform;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rb.AddForce(new Vector2(_travelHorizontal, 0));
    }
    private void FixedUpdate()
    {
        /*transform.position = new Vector2(transform.position.y, transform.position.x + _movingSpeed);
        if(transform.position.x >= _travelHorizontal)
        {
            transform.position = _startTransform.position;
        }*/
    }
}
