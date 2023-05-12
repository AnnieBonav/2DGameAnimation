using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reservoir : MonoBehaviour
{
    [SerializeField] private int _maxReservoire;
    [SerializeField] private Image _reservoirImage;
    private float _currentReservoire;

    private void Awake()
    {
        _currentReservoire = _maxReservoire;
        StartCoroutine(IncreaseHealth());
        Projectile.HitPlayer += AddDamage;
    }

    private void OnDisable()
    {
        StopCoroutine(IncreaseHealth());
    }

    private IEnumerator IncreaseHealth()
    {
        while (true)
        {
            if(_currentReservoire <= _maxReservoire)
            {
                _currentReservoire += 0.2f;
            }
            yield return new WaitForSeconds(3);
        }
    }

    private void AddDamage(float damage)
    {
        _currentReservoire -= damage;
        if( _currentReservoire < 0)
        {
            print("LIKE, DIED");
        }
    }

    private void Update()
    {
        float calculateFill = _currentReservoire / _maxReservoire;
        _reservoirImage.fillAmount = Mathf.MoveTowards(_reservoirImage.fillAmount, calculateFill, Time.deltaTime/10);
    }
}
