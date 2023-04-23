using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RotatingWorld
{
    public class ShakeCamera : MonoBehaviour
    {
        private float _duration = 5f;
        [SerializeField] private GameObject _cineMachine;

        private void Awake()
        {
            XolotlController.ShakeCamera += Activate;
        }

        private void Activate()
        {
            _cineMachine.SetActive(false);
            StartCoroutine(Shake());
            print("Activated Shake");
        }

        private IEnumerator Shake()
        {
            Vector3 startPosition = transform.position;
            float elapsedTime = 0f;

            while(elapsedTime < _duration)
            {
                elapsedTime += Time.deltaTime;
                transform.position = startPosition + UnityEngine.Random.insideUnitSphere;
                yield return null;
            }

            transform.position = startPosition;

        }

    }
}


