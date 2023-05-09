using System;
using System.Collections;
using Spine.Unity;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RotatingWorld
{
    public class XolotlController : MonoBehaviour
    {
        public static event Action ShakeCamera;

        [SpineAnimation][SerializeField] private string _idleAnimation;
        [SpineAnimation][SerializeField] private string _runAnimation;
        [SpineAnimation][SerializeField] private string _heavyBreathingAnimation;

        private SkeletonAnimation _skeletonAnimation;
        [SerializeField] private Spine.AnimationState _animationState;
        [SerializeField] private Spine.Skeleton _skeleton;

        [SerializeField] private float _jumpForce = 200f;
        [SerializeField] private bool _verbose = true;
        [SerializeField] private bool _animationsVerbose = true;
        private Rigidbody2D _rb;
        private Vector2 _movement;

        public enum PlayerState { Idle, Walking, Running, HeavyBreathing, Jumping };
        private PlayerState _playerState = PlayerState.Idle;

        private bool _facingRight = false;
        private float _direction;

        private bool animate = false;
        private int turns = 5;
        private Animation _animation;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animation = GetComponent<Animation>();
            ExitPlate.TouchedExitPlate += CompletedPuzzle;
        }

        private void ChangeState(PlayerState newState)
        {
            switch (newState)
            {
                case PlayerState.Idle:
                    if (_playerState != PlayerState.Idle)
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

        private void Start()
        {
            _skeletonAnimation = GetComponent<SkeletonAnimation>();
            _animationState = _skeletonAnimation.AnimationState;
            _skeleton = _skeletonAnimation.Skeleton;

            _animationState.SetAnimation(0, _idleAnimation, true);
        }

        private void FixedUpdate()
        {
        }

        public void OnJump(InputValue value)
        {
           if(_verbose) print("Jumped");
            _rb.AddForce(new Vector2(0, _jumpForce));
        }
        public void OnRotate(InputValue value)
        {
            Vector2 inputVector = value.Get<Vector2>();
            _movement = new Vector2(inputVector.x, 0f);
            _direction = _movement.x;

            if (_movement.x != 0)
            {
                CheckFlip();
                ChangeState(PlayerState.Running);
            }
            if (_movement.x == 0)
            { // If there is no movement, change to idle?
                if(_animationsVerbose) print("Change idle");
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
        private void CompletedPuzzle()
        {
            ShakeCamera.Invoke();
            StartCoroutine(PlayAndWaitForAnimation());

        }
        private IEnumerator PlayAndWaitForAnimation()
        {
            _animation.Play();
            while (_animation.isPlaying)
            {
                yield return null;
            }
            print("Would change scene");
            SceneManager.LoadScene(2);
        }
        /*
        private IEnumerator Ascend()
        {
            while(turns > 0)
            {
                _rb.AddForce(new Vector2(0, 1));
                turns--;
                yield return new WaitForSeconds(1f);
            }

        }*/
    }
}


