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
    [SerializeField] private int _projectilesAmount;
    [SerializeField] private GameObject _projectilePrefab;
    private Vector2 _ejectionOrigin; // Ejected origin is only calculated once. I could create a line that it follows and then update the ejection origin so the logic of the ejection origin is separated from the logic of ejecting
    private ObjectPool<Projectile> _projectilePool;

    private void Awake()
    {
        _projectilePool = new ObjectPool<Projectile>(_projectilesAmount, _projectilePrefab);
        _ejectionOrigin = _originTransform.position;
    }


    private void Start()
    {
        StartCoroutine("EjectArchProjectile");
    }

    public void InstantiateProjectile()
    {
        Projectile projectile = _projectilePool.GetPooledObject();
        if (projectile != null)
        {
            projectile.transform.position = _ejectionOrigin;
            projectile.gameObject.SetActive(true);
        }
    }

    private IEnumerator EjectArchProjectile() // int minAngle, int maxAngle
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            InstantiateProjectile();
        }
    }
}
