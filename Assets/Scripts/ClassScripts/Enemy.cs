using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int damage = 5;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out IDamageable damageableObj))
        {
            damageableObj.TakeDamage(damage);
        }
    }
}
