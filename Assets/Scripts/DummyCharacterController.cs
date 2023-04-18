using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DummyCharacterController : MonoBehaviour
{
    Rigidbody2D _rigidBody;
    [SerializeField] private float _speed = 10;
    private Vector2 _movement;

    [SerializeField] private float _jumpForce = 20f;

    [SerializeField] private Spine.AnimationState _animationState;
    [SerializeField] private Spine.Skeleton _skeleton;

    public enum PlayerState { Idle, Walking, Running, HeavyBreathing, Jumping };
    private PlayerState _playerState = PlayerState.Idle;

    private bool _facingRight = false;
    private float _direction;


    private void ChangeState(PlayerState newState)
    {
        switch (newState)
        {
            case PlayerState.Idle:
                print("Idle");
                _playerState = newState;
                _rigidBody.velocity = new Vector2(0, 0);
                break;
            case PlayerState.Running:
                if (_playerState != PlayerState.Running)
                {
                    print("Running");
                    _playerState = newState;
                }
                else
                {
                    print("Else");
                }
                break;
        }
    }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        _rigidBody.AddForce(_movement * _speed);
        if (_movement.x == 0)
        {
            ChangeState(PlayerState.Idle);
        }
    }

    public void OnJump(InputValue value)
    {
        Debug.Log("Called Jump");
        Jump();
    }
    private void Jump()
    {
        _rigidBody.AddForce(new Vector2(0, _jumpForce));
    }

    public void OnFire(InputValue value)
    {
        Debug.Log("Called Fire");
    }

    public void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        _movement = new Vector2(inputVector.x, 0f);
        if (_movement.x != 0)
        {
            _direction = _movement.x;
            Flip();
            ChangeState(PlayerState.Running);// In the future it can be plus a certain parameter to change between walk and run
        }
    }

    private void Flip()
    {
        if (_facingRight && _direction < 0f || !_facingRight && _direction > 0f)
        {
            _facingRight = !_facingRight;
            transform.Rotate(0, 180f, 0);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.name);
    }
}
