using UnityEngine;
using Spine.Unity;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.XR;

public class SimplePlayerController : MonoBehaviour
{
    Rigidbody2D _rigidBody;
    [SerializeField] private float _speed = 10;
    private Vector2 _movement;

    [SerializeField] private float _jumpForce = 200f;

    [SpineAnimation] [SerializeField] private string _idleAnimation;
    [SpineAnimation] [SerializeField] private string _runAnimation;
    [SpineAnimation] [SerializeField] private string _heavyBreathingAnimation;

    private SkeletonAnimation _skeletonAnimation; 
    [SerializeField] private Spine.AnimationState _animationState;
    [SerializeField] private Spine.Skeleton _skeleton;

    public enum PlayerState { Idle, Walking, Running, HeavyBreathing, Jumping};
    private PlayerState _playerState = PlayerState.Idle;

    private bool _facingRight = false;
    private float _direction;


    private void ChangeState(PlayerState newState)
    {
        switch (newState)
        {
            case PlayerState.Idle:
                if(_playerState != PlayerState.Idle)
                {
                    _playerState = newState;
                    _animationState.SetAnimation(0, _idleAnimation, true);
                }
                break;
            case PlayerState.Running:
                if (_playerState != PlayerState.Running)
                {
                    _playerState = newState;
                    _animationState.SetAnimation(0, _runAnimation, true);
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
        _skeletonAnimation = GetComponent<SkeletonAnimation>();
        _animationState = _skeletonAnimation.AnimationState;
        _skeleton = _skeletonAnimation.Skeleton;

        _animationState.SetAnimation(0, _idleAnimation, true);
    }

    private void FixedUpdate()
    {
        _rigidBody.AddForce(_movement * _speed);
        if(_movement.x == 0)
        {
            ChangeState(PlayerState.Idle);
        }
    }

    public void OnJump(InputValue value)
    {
        Debug.Log("Called Jump");
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
        // print("On Move: " + _movement);
        _direction = _movement.x;
        
        if (_movement.x != 0)
        {
            CheckFlip();
            ChangeState(PlayerState.Running);
        }
        if(_movement.x == 0) { // If there is no movement, change to idle?
            print("Change idle");
            ChangeState(PlayerState.Idle);
        }
    }

    private void CheckFlip()
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

    IEnumerator MyCoroutine()
    {
        // Yield retunr null wiats til next frame
        _animationState.SetAnimation(0, _runAnimation, true);
        yield return new WaitForSeconds(2f);
        _animationState.SetAnimation(0, _idleAnimation, true);
        yield return new WaitForSeconds(2f);
    }
}
