using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyProjectilePool : MonoBehaviour
{
    /*private List<Projectile> _pooledProjectiles;
    private Projectile _prefabToPool; // A prefab is a aGame Object :0
    private int _poolSize;

    public OnlyProjectilePool(int amountToPool, GameObject prefabToPool)
    {
        _poolSize = amountToPool;
        _prefabToPool = prefabToPool; // Sent through whoever instantiates the Object Poo;
        InitializePool();
    }

    private void InitializePool()
    {
        _pooledProjectiles = new List<Projectile>();
        GameObject temporaryProjectile;
        for (int i = 0; i < _poolSize; i++)
        {
            temporaryProjectile = Object.Instantiate(_prefabToPool);
            temporaryProjectile.gameObject.SetActive(false);
            temporaryProjectile.Add(temporaryProjectile);
        }

    }

    public T GetPooledObject()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            if (!_pooledObjects[i].gameObject.activeInHierarchy)
            {
                return _pooledObjects[i];
            }
        }
        return default(T);
    }*/
}
