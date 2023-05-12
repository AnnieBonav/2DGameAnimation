using UnityEngine;
using UnityEngine.InputSystem;


namespace RotatingWorld
{
    public class WorldController : MonoBehaviour
    {
        [SerializeField] private bool _verbose;
        [SerializeField] private float _rotationSpeed = 100;

        [Header("Change day settings")]
        [SerializeField] private ChangeDay _changeDay;

        private Transform _transform;
        private Vector2 _movement;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            ExitPlate.TouchedExitPlate += CompletedPuzzle;

        }
        private void FixedUpdate()
        {
            float rotationChange = _movement.x * _rotationSpeed * Time.deltaTime;
            _changeDay.UpdateAngle(rotationChange);
            _transform.Rotate(0, 0, rotationChange);
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

