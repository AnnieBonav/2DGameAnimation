using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// Launches projectile either cylinder inside out, cylinder oustide in, a predefined arc,
// Handles direction and speed
// TODO: Add option to either be or not be affected by gravity (the projectiles launched)

[RequireComponent(typeof(MeshCollider))]
public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private int _projectilesAmount;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _secondsBetweenProjectiles = 5f;
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _xLeftMargin = -20;
    [SerializeField] private float _yLeftMargin = 15;

    private Vector2 _ejectionOrigin;
    private ObjectPool<Projectile> _projectilePool;

    private void Awake()
    {
        _projectilePool = new ObjectPool<Projectile>(_projectilesAmount, _projectilePrefab);
        _ejectionOrigin = transform.position;
    }


    private void Start()
    {
        StartCoroutine("EjectArchProjectile");
    }

    private IEnumerator EjectArchProjectile() // int minAngle, int maxAngle
    {
        while (true)
        {
            InstantiateProjectile();
            yield return new WaitForSeconds(_secondsBetweenProjectiles);
        }
    }

    public void InstantiateProjectile()
    {
        Projectile projectile = _projectilePool.GetPooledObject();
        // Vector2 randomDirection = UnityEngine.Random.insideUnitCircle;
        if (projectile != null)
        {
            // projectile.gameObject.SetActive(true);
            projectile.Activate(_ejectionOrigin, _targetTransform);
        }
    }

    private void FixedUpdate()
    {
        MoveLauncher();
    }

    private void MoveLauncher()
    {
        float randomX = UnityEngine.Random.Range(_xLeftMargin, _yLeftMargin);
        _ejectionOrigin = new Vector2(randomX, transform.position.y);
    }
}
