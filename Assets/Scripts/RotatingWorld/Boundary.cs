using System;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    public static event Action<bool> OnBoundaryCollision;
    [SerializeField] private bool _futureBoundary;

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnBoundaryCollision?.Invoke(_futureBoundary);
    }
}
