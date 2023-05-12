using System;
using System.Collections;
using Spine.Unity;
using UnityEngine.InputSystem;
using UnityEngine;
using Spine.Unity.AttachmentTools;
using UnityEngine.SceneManagement;

namespace RotatingWorld
{
    public class XolotlController : MonoBehaviour
    {
        public static event Action ShakeCamera;

        [SpineAnimation][SerializeField] private string _idleAnimation;
        [SpineAnimation][SerializeField] private string _runAnimation;
        [SpineAnimation][SerializeField] private string _heavyBreathingAnimation;
        [SpineAnimation][SerializeField] private string _levitateAnimation;

        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        [SerializeField] private Spine.AnimationState _animationState;
        [SerializeField] private Spine.Skeleton _skeleton;

        [SerializeField] private float _jumpForce = 200f;
        [SerializeField] private bool _verbose = true;
        [SerializeField] private bool _animationsVerbose = true;
        private Rigidbody2D _rb;
        private Vector2 _movement;

        public enum PlayerState { Idle, Walking, Running, HeavyBreathing, Jumping, Levitating };
        private PlayerState _playerState = PlayerState.Idle;

        private bool _facingRight = false;
        private float _direction;

        private bool animate = false;
        private int turns = 5;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            ExitPlate.TouchedExitPlate += CompletedPuzzle;
        }

        private void Start()
        {
            _animationState = _skeletonAnimation.AnimationState;
            _skeleton = _skeletonAnimation.Skeleton;

            _animationState.SetAnimation(0, _idleAnimation, true);

            //Spine.TrackEntry trackEntry = _animationState.SetAnimation(0, "Main/Levitate", true);
            //trackEntry.End += OnSpineAnimationEnd;

            _animationState.Complete += OnSpineAnimationCompleted;
        }

        private void ChangeState(PlayerState newState)
        {
            if (_playerState == PlayerState.Levitating) return; // Fix to not change back to anotehr
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
                case PlayerState.Levitating:
                    if(_playerState != PlayerState.Levitating)
                    {
                        print("Changing animation");
                        _playerState = newState;
                        _animationState.SetAnimation(0, _levitateAnimation, false);
                    }
                    break;
            }
        }
        
        public void OnJump(InputValue value)
        {
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
            {
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
            ChangeState(PlayerState.Levitating);
        }

        private void OnSpineAnimationCompleted(Spine.TrackEntry trackEntry)
        {
            if(trackEntry.ToString() == "Main/Levitate")
            {
                print("Finished levitate");
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}


