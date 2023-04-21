using UnityEngine;
using UnityEngine.InputSystem;


namespace RotatingWorld
{
    public class WorldController : MonoBehaviour
    {
        [SerializeField] private bool _verbose;
        [SerializeField] private float _rotationSpeed = 100;
        private Rigidbody2D _rb;
        private Transform _transform;
        private Vector2 _movement;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _transform = GetComponent<Transform>();
        }

        private void FixedUpdate()
        {
            _transform.Rotate(0, 0, _movement.x * _rotationSpeed * Time.deltaTime);
        }

        public void OnRotate(InputValue value)
        {
            if (_verbose) print("Rotate");
            _movement = value.Get<Vector2>();
        }
    }
}

