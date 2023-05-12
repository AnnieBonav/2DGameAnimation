using System;
using UnityEngine;

namespace ClassStuff
{
    public class CharacterController : MonoBehaviour, IDamageable
    {
        // public static event Action Jumped;

        private float _direction;
        [SerializeField]
        private float speed = .1f;

        [SerializeField]
        private float jumpHeight = 7f;

        private Rigidbody2D _rb;
        private bool _isGrounded = false;
        private Animator _animator;
        private bool _facingRight = true;
        MovementState _state;
        private int _health = 10;

        bool _immovable = false;

        private enum MovementState { idle, running, jumping, falling, attacking, died}

        public void Die()
        {
            _state = MovementState.died;
            _animator.SetInteger("State", (int)_state);

            print("Died, woops. State: " + _state + "Something: " + _animator.GetInteger("State"));

        }
        public void TakeDamage(int damage)
        {
            _health -= damage;
            print(_health);
            if(_health <= 0)
            {
                Die();
            }
        }

        void Awake()
        {
            _animator = GetComponent<Animator>();
            _state = 0;
        }

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void SwitchImmovable()
        {
            _immovable = !_immovable;
         
        }

        public void Attack()
        {
            print("I would be attacking...if I had only implemented attacking aaaaaaaaaaa");
        }

        private void Update()
        {
            if (_state == MovementState.died) return;
            CheckMovement();
        }

        private void CheckMovement()
        {
            if (!_immovable)
            {
                _direction = Input.GetAxis("Horizontal");
                _animator.SetFloat("Speed", Math.Abs(_direction));
                transform.Translate(transform.right * _direction * speed * Time.deltaTime);

                if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && _isGrounded)
                {
                    _isGrounded = false;
                    Jump();
                }
                Flip();
            }
            UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            if(_direction != 0)
            {
                _state = MovementState.running;
            }
            else if (_isGrounded == true)
            {
                _state = MovementState.idle;
            }

            if(_rb.velocity.y > 0.1f)
            {
                _state = MovementState.jumping;
            }else if (_rb.velocity.y < -0.1f)
            {
                 _state = MovementState.falling;
            }

            if(_isGrounded && Input.GetKeyDown(KeyCode.Mouse0))
            {
                _state = MovementState.attacking;
            }

            print(_state);
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
            if(collision.gameObject.tag == "Floor")
            {
                _isGrounded = true;
            }
        }

        private void Jump()
        {
            _rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);

        }
    }

}