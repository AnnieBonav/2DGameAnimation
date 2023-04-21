using UnityEngine;
using UnityEngine.InputSystem;


public class GravityPlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] float _speed = 50;
    private Vector2 _impulse;
    [SerializeField] float _jumpForce = 100f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // transform.Translate(_impulse * (Time.deltaTime * _speed), Space.Self);
        _rb.AddForce(_impulse * (Time.deltaTime * _speed));
        /* else
        {
            transform.Translate(Vector2.zero, Space.Self);
        }*/
    }

    public void OnMove(InputValue value)
    {
        Vector2 inputVector = value.Get<Vector2>();
        _impulse = inputVector;
    }

    public void OnJump(InputValue value)
    {
        Vector2 jumpVector = new Vector2(0, _jumpForce);
        _rb.AddForce(jumpVector);

        print("Jump: " + jumpVector + "Rb: " + _rb.velocity);
    }
}
