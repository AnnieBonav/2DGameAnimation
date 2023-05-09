using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPlate : MonoBehaviour
{
    public static event Action TouchedExitPlate;

    private void Awake()
    {
        Level.CompletedPuzzle += Activate;
        this.gameObject.SetActive(false);
    }

    private void Activate()
    {
        this .gameObject.SetActive(true);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        TouchedExitPlate?.Invoke();
        //print("You completed the puzzle!");
        //SceneManager.LoadScene(2);
    }
}
