using UnityEngine;
using UnityEngine.InputSystem;

public class DummyCharacterController : MonoBehaviour
{
    [SerializeField] private Transform _pointACoordinates;
    [SerializeField] private Transform _pointBCoordinates;
    private Vector2 _movement;
    private Transform _transform;

    Rigidbody2D _rigidBody;
    [SerializeField] private float _speed = 200;
    [SerializeField] private float _jumpForce = 200f;
    private bool _goingRight;

    [SerializeField] private bool _verbose;

    private Vector2 _impulse = new Vector2(0,0);

    private Vector2 _normal;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        /* if(_goingRight)
        {
            _rigidBody.AddForce(new Vector2(Vector2.right, (float)Space.Self));
            // _movement = new Vector2(inputVector.x * _speed, (float)Space.Self);
        }*/
        
        // print("velocity:  " + _rigidBody.velocity + "rb velocity magnitude: " + _rigidBody.velocity.magnitude + "force:" + _rigidBody.angularVelocity);
        

        if (_impulse == _normal) return;
        Calculate_normal();
        Vector2 addingForce = _movement * _speed;
        print("Movement: " + addingForce);
        _rigidBody.AddForce(addingForce) ;
    }

    public void OnJump(InputValue value)
    {
        _rigidBody.AddForce(new Vector2(0, _jumpForce));
    }

    public void OnFire(InputValue value)
    {
        print("Called Fire");
    }

    public void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        //_movement = new Vector2(inputVector.x * _speed, (float)Space.Self);
        _impulse = inputVector;
    }

    private void Calculate_normal()
    {
        
        Vector2 pointA = _pointACoordinates.position;
        Vector2 pointB = _pointBCoordinates.position;

        float dx = pointA.x - pointB.x;
        float dy = pointA.y - pointB.y;
        Vector2 _normal = new Vector2(-dy, dx);

        if (_impulse == _normal) return;

        print("_normal: " + _normal + "Impulse: " + _impulse);

        float magnitudeImpulse = Mathf.Sqrt( (_impulse.x * _impulse.x) + (_impulse.y * _impulse.y));
        float magnitude_normal = Mathf.Sqrt( (_normal.x * _normal.x) + (_normal.y * _normal.y));
        float angleBetweenVectors = Mathf.Acos(Vector2.Dot(_impulse, _normal) / (magnitudeImpulse * magnitude_normal) ) * 180 / Mathf.PI;

        print("Angle: " + angleBetweenVectors);
        _movement = new Vector2(Mathf.Cos(angleBetweenVectors), Mathf.Sin(angleBetweenVectors));

        print("Cos: " + Mathf.Cos(angleBetweenVectors) + "Sin: " + Mathf.Sin(angleBetweenVectors));
    }
}
