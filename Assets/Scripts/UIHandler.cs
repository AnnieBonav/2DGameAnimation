using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lapsText;

    private void Awake()
    {
        Level.OnLapsChanged += ChangeLapsText;
    }

    private void ChangeLapsText(int newLaps)
    {
        _lapsText.text = newLaps.ToString();
    }
}
