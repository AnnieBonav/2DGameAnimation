using UnityEngine;
using Spine.Unity;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rigidBody;
    [SerializeField] private float _speed = 10;
    private Vector2 _movement;

    [SerializeField] private float _jumpForce = 20f;

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
        _playerState = newState;
        switch (newState)
        {
            case PlayerState.Idle:
                _animationState.SetAnimation(0, _idleAnimation, true);
                // _rigidBody.angularVelocity = 0;
                // _rigidBody.inertia = 0;
                _rigidBody.velocity = new Vector2(0, 0);
                break;
            case PlayerState.Running:
                _animationState.SetAnimation(0, _runAnimation, true);
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

        _animationState.SetAnimation(0, _runAnimation, true);
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
        print("Moving?: " + inputVector);
        _movement = new Vector2(inputVector.x, 0f);
        if(_movement.x != 0) {
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

    IEnumerator MyCoroutine()
    {
        // Yield retunr null wiats til next frame
        _animationState.SetAnimation(0, _runAnimation, true);
        yield return new WaitForSeconds(2f);
        _animationState.SetAnimation(0, _idleAnimation, true);
        yield return new WaitForSeconds(2f);
    }
}
