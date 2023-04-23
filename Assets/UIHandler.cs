using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    private TextMeshProUGUI _lapsText;
    private void Awake()
    {
        Level.OnLapsChanged += ChangeLapsText;
        _lapsText = GetComponent<TextMeshProUGUI>();
    }

    private void ChangeLapsText(int newLaps)
    {
        _lapsText.text = newLaps.ToString();
    }
}
