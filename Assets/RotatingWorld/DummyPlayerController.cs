using UnityEngine;
using UnityEngine.InputSystem;

namespace RotatingWorld
{
    public class DummyPlayerController : MonoBehaviour
    {
        [SerializeField] private bool _verbose;
        [SerializeField] private float _jumpForce = 200;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void OnJump(InputValue value)
        {
            if(_verbose) print("Jumped");
            _rb.AddForce(new Vector2(0, _jumpForce));
        }
    }

}

