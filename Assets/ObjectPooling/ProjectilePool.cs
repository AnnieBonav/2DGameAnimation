using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : ObjectPool<Projectile>
{
    public ProjectilePool(int amountToPool, GameObject prefabToPool) : base(amountToPool, prefabToPool)
    {
    }
}
