using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDayBasedTime : MonoBehaviour
{
    [SerializeField] MeshRenderer _meshRenderer;
    private Material _horizonMaterial;
    private float _scale = 0f;
    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _horizonMaterial = _meshRenderer.material;
        // _horizonMaterial.SetFloat("_Scale", 2); // Works!
    }

    private void FixedUpdate()
    {
        // _scale -= Time.deltaTime * 0.00001f; // Nope
        _scale = _scale - 0.0001f;
        _horizonMaterial.SetFloat("_Offset", _scale);
        if (_scale < -5f)
        {
            _scale = 2f;
        }
    }
}
