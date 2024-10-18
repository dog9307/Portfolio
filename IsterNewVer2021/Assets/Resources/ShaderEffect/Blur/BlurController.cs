using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurController : MonoBehaviour
{
    [SerializeField]
    private Renderer _relativeRenderer;

    private Material _mat;

    [HideInInspector]
    [SerializeField]
    private float _currentAmount = 0.0f;
    public float currentAmount { get { return _currentAmount; } set { _currentAmount = value; } }

    [SerializeField]
    private float _startAmount = 0.71f;
    private float _prevAmount;

    private float _nextAmount;
    public float nextAmount { get { return _nextAmount; } set { _nextAmount = value; } }

    void Awake()
    {
        if (!_relativeRenderer)
        {
            _relativeRenderer = GetComponent<Renderer>();
            if (!_relativeRenderer)
            {
                Destroy(this);
                return;
            }
        }

        _mat = _relativeRenderer.material;
    }

    void Start()
    {
        _prevAmount = _startAmount;
        ApplyBlur(_startAmount);
    }

    void Update()
    {
        if (_prevAmount != _currentAmount)
        {
            ApplyBlur(_currentAmount);
            _prevAmount = _currentAmount;
        }
    }

    public void ApplyBlur(float amount)
    {
        _currentAmount = amount;
        _mat.SetFloat("_BlurAmount", _currentAmount);
    }
}
