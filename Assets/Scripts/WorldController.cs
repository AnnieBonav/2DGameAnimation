using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorldController : MonoBehaviour
{
    private Vector2 _movement;
    private Transform _transform;

    [SerializeField] private float _rotationSpeed = 5f;

    public void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    public void FixedUpdate()
    {
        //Debug.Log("Here");
        _transform.Rotate(0, 0, _movement.x * _rotationSpeed * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        _movement = new Vector2(inputVector.x, inputVector.y).normalized;
    }
}
