using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObjectPool : MonoBehaviour
{
    public static StaticObjectPool ObjectPoolInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    private void Awake()
    {
        ObjectPoolInstance = this;
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }

    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}

/*
 // Calling it
 GameObject arrow = ObjectPool.ObjectPoolInstance.GetPooledObject();
        if (arrow != null)
        {
            arrow.transform.position = _ejectionOrigin;
            arrow.SetActive(true);
}*/
