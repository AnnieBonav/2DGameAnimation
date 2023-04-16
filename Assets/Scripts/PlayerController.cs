using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rigidBody;
    [SerializeField] private float _jumpForce = 5f;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        
    }

    public void OnJump(InputValue value)
    {
        Debug.Log("Called Jump");
        Jump();
    }

    public void OnFire(InputValue value)
    {
        // Debug.Log("Called Fire");
    }

    private void Jump()
    {
        _rigidBody.AddForce(new Vector2(0, _jumpForce));
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.name);
    }
}
