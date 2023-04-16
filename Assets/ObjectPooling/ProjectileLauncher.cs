using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// Launches projectile either cylinder inside out, cylinder oustide in, a predefined arc,
// Handles direction and speed

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private Camera _camera;
    private int _rayDistance = 300;
    private void Awake()
    {
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // TODO: Change to OnFire
        {
            Vector3 position = _camera.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            InstantiateProjectile(position);
            print("Input: " + Input.mousePosition + "Position: " + position);
        }
    }

    public void InstantiateProjectile(Vector3 vector)
    {
        GameObject arrow = ObjectPool.ObjectPoolInstance.GetPooledObject();
        if (arrow != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit rayHit;
            if (!Physics.Raycast(ray, out rayHit))
            {
                arrow.transform.position = vector;
                arrow.SetActive(true);                
            }
        }
    }
}
