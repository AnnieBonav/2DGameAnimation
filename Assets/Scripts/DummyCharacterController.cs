using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DummyCharacterController : MonoBehaviour
{
    Rigidbody2D _rigidBody;
    [SerializeField] private float _speed = 10;

    [SerializeField] private float _jumpForce = 20f;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    public void OnJump(InputValue value)
    {
        _rigidBody.AddForce(new Vector2(0, _jumpForce));
    }

    public void OnFire(InputValue value)
    {
        print("Called Fire");
    }
}
