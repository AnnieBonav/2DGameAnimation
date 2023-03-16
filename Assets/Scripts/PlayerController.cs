using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static event Action Jumped;

    private float _direction;
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float jumpHeight = 7f;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private Animator _animator;
    private bool _facingRight = true;
    MovementState _state;

    private enum MovementState { idle, running, jumping }

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _state = 0;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Debug.Log("State: " + _state.ToString());
        _direction = Input.GetAxis("Horizontal");
        _animator.SetFloat("Speed", Math.Abs(_direction));
        if (_direction != 0)
        {
            _animator.SetBool("IsRunning", true);
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }

        transform.Translate(transform.right * _direction * speed * Time.deltaTime);
        if ( (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            Jump();
            _state = MovementState.jumping;
            _animator.SetInteger("State", (int)_state);
            isGrounded = false;
        }

        Flip();
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        if(_direction != 0)
        {
            _state = MovementState.running;
        }
        else if (isGrounded == true)
        {
            _state = MovementState.idle;
        }

        _animator.SetInteger("State", (int)_state);
    }

    private void Flip()
    {
        if(_facingRight && _direction < 0f || !_facingRight && _direction > 0f)
        {
            _facingRight = !_facingRight;
            transform.Rotate(0, 180f, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Floor")
        {
            Debug.Log("Is checked");
            isGrounded = true;
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);

    }
}
