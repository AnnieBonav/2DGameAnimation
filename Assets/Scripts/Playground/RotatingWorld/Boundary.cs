using System;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    public static event Action<Direction> OnBoundaryCollision;
    [SerializeField] private Tense _tense;
    private Direction _direction;

    private void Awake()
    {
        _direction = new Direction(_tense);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("collided player");

            OnBoundaryCollision?.Invoke(_direction);
        }
    }
}
