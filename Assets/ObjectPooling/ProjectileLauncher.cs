using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// Launches projectile either cylinder inside out, cylinder oustide in, a predefined arc,
// Handles direction and speed

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private void Start()
    {
        StartCoroutine("EjectArchProjectile");
    }
    public void OnFire()
    {
        Vector3 position = _camera.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        InstantiateProjectile(position);
        print("Input: " + Input.mousePosition + "Position: " + position);
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

    private IEnumerator EjectArchProjectile() // int minAngle, int maxAngle
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            print("Eject");
            InstantiateProjectile(new Vector2(0,0));
        }
    }
}
