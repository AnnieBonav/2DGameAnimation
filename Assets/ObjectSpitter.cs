using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spits <object> either cylinder inside out, cylinder oustide in, a predefined arc,
public class ObjectSpitter : MonoBehaviour
{
    [SerializeField] GameObject _object; // Object to spit
    [SerializeField] int _poolSize; // Amount of objects that will exist
}
