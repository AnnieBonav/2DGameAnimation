using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool: MonoBehaviour
{
    private List<GameObject> _pooledProjectiles;
    private GameObject _prefabToPool; // A prefab is a aGame Object :0
    private int _poolSize;

    public ProjectilePool(int amountToPool, GameObject prefabToPool)
    {
        _poolSize = amountToPool;
        _prefabToPool = prefabToPool; // Sent through whoever instantiates the Object Poo;
        InitializePool();
    }

    private void InitializePool()
    {
        _pooledProjectiles = new List<GameObject>();
        GameObject temporaryProjectile;
        for (int i = 0; i < _poolSize; i++)
        {
            temporaryProjectile = Instantiate(_prefabToPool);
            temporaryProjectile.gameObject.SetActive(false);
            _pooledProjectiles.Add(temporaryProjectile);
        }

    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            if (!_pooledProjectiles[i].gameObject.activeInHierarchy)
            {
                return _pooledProjectiles[i];
            }
        }
        return null;
    }
}
