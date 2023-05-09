using UnityEngine;
using UnityEngine.InputSystem;


namespace RotatingWorld
{
    public class WorldController : MonoBehaviour
    {
        [SerializeField] private bool _verbose;
        [SerializeField] private float _rotationSpeed = 100;
        private Transform _transform;
        private Vector2 _movement;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            ExitPlate.TouchedExitPlate += CompletedPuzzle;
        }
        private void FixedUpdate()
        {
            _transform.Rotate(0, 0, _movement.x * _rotationSpeed * Time.deltaTime);
        }
        private void CompletedPuzzle()
        {
            _movement = Vector2.zero;
        }
        public void OnRotate(InputValue value)
        {
            if (_verbose) print("Rotate");
            _movement = value.Get<Vector2>();
        }
    }
}

