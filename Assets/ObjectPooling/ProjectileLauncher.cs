using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// Launches projectile either cylinder inside out, cylinder oustide in, a predefined arc,
// Handles direction and speed
// TODO: Add option to either be or not be affected by gravity (the projectiles launched)

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _originTransform;
    [SerializeField] private int _projectilesAmount;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _secondsBetweenProjectiles = 5f;

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
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle;
        if (projectile != null)
        {
            projectile.Activate(_ejectionOrigin, randomDirection);
        }
    }
}
