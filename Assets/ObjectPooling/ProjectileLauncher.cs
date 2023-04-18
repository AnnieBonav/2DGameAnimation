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
    [SerializeField] private Transform _originTransform;
    private Vector2 _ejectionOrigin; // Ejected origin is only calculated once. I could create a line that it follows and then update the ejection origin so the logic of the ejection origin is separated from the logic of ejecting

    private void Awake()
    {
        _ejectionOrigin = _originTransform.position;
    }


    private void Start()
    {
        StartCoroutine("EjectArchProjectile");
    }

    public void InstantiateProjectile()
    {
        GameObject arrow = ObjectPool.ObjectPoolInstance.GetPooledObject();
        if (arrow != null)
        {
            arrow.transform.position = _ejectionOrigin;
            arrow.SetActive(true);                
        }
    }

    private IEnumerator EjectArchProjectile() // int minAngle, int maxAngle
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            print("Eject");
            InstantiateProjectile();
        }
    }
}
