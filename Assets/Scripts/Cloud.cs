using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float _speed = 1;

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + _speed, transform.position.y);
    }
}
