using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RotatingWorld
{
    public class ShakeCamera : MonoBehaviour
    {
        [SerializeField] private float _duration = 2f;
        [SerializeField] private GameObject _cineMachine;
        [SerializeField] private float _intensity = 0.5f;
        [SerializeField] private bool _shake = false;

        private void Awake()
        {
            XolotlController.ShakeCamera += Activate;
        }

        private void Activate()
        {
            if (!_shake) return;
            _cineMachine.SetActive(false);
            StartCoroutine(Shake());
        }

        private IEnumerator Shake()
        {
            yield return new WaitForSeconds(1.5f);
            Vector3 startPosition = transform.position;
            float elapsedTime = 0f;

            while(elapsedTime < _duration)
            {
                elapsedTime += Time.deltaTime;
                transform.position = startPosition + UnityEngine.Random.insideUnitSphere * _intensity;
                yield return null;
            }

            transform.position = startPosition;

        }

    }
}


