using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeDay : MonoBehaviour
{
    [SerializeField] MeshRenderer _meshRenderer;
    private Material _horizonMaterial;
    private float _scale = 0f;
    private float _angle;

    [SerializeField]  private float _upperLimitOffset = 2f;
    [SerializeField]  private float _lowerLimitOffset = 0f;
    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _horizonMaterial = _meshRenderer.material;
        // _horizonMaterial.SetFloat("_Scale", 2); // Works!
    }

    public void UpdateAngle(float angle)
    {
        _angle += angle;
    }

    private float ConvertFrom_Range1_Input_To_Range2_Output(float _input_range_min, float _input_range_max, float _output_range_min, float _output_range_max, float _input_value_tobe_converted)
    {
        float diffOutputRange = Mathf.Abs((_output_range_max - _output_range_min));
        float diffInputRange = Mathf.Abs((_input_range_max - _input_range_min));
        float convFactor = (diffOutputRange / diffInputRange);
        return (_output_range_min + (convFactor * (_input_value_tobe_converted - _input_range_min)));
    }

    private void FixedUpdate()
    {

        _scale = ConvertFrom_Range1_Input_To_Range2_Output(-720, 720, -0.5f, 6, _angle);
        _horizonMaterial.SetFloat("_Offset", _scale);
        /*if (_scale < -5f)
        {
            _scale = 2f;
        }*/
    }
}
