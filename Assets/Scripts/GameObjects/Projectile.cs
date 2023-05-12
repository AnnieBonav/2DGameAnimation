using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static event Action<float> HitPlayer;
    [SerializeField] private float _damage = 5;
    private float _thrust = 10;

    private Vector2 _destination;
    public void Activate(Vector2 ejectOrigin, Transform target)
    {
        gameObject.SetActive(true);
        transform.position = ejectOrigin;
        _destination = target.position;

        float randomX = UnityEngine.Random.Range(-10, 10);
        _destination.x = randomX;
        _destination.y -= 20;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        HandleMove();
        HandleRotation();
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name is "Boundary")
        {
            Deactivate();
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")) {
            HandleHitPlayer();
            Deactivate();
        }else if (collider.CompareTag("World"))
        {
            Deactivate();
        }
    }

    private void HandleHitPlayer()
    {
        HitPlayer?.Invoke(_damage);
    }

    public void HandleMove()
    {
        transform.position = Vector2.MoveTowards(transform.position, _destination, Time.deltaTime * _thrust);
    }

    public void HandleRotation()
    {
        transform.up = _destination - (Vector2)transform.position;
    }
}
