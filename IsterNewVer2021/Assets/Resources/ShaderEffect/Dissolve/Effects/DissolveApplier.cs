using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveApplier : MonoBehaviour
{
    [SerializeField]
    private Color _color;
    [SerializeField]
    private float _intensity;
    [SerializeField]
    private float _scale = 10.0f;
    
    [SerializeField]
    private float _currentFade;
    public float currentFade { get { return _currentFade; } set { _currentFade = value; } }
    [HideInInspector]
    public bool _dissolveOn;

    private Renderer _renderer;

    void Start()
    {
        _renderer = GetComponent<Renderer>();

        _renderer.material.SetColor("_DissolveColor", _color * _intensity);
        _renderer.material.SetFloat("_Fade", currentFade);
        _renderer.material.SetFloat("_Scale", _scale);
    }

    void Update()
    {
        _renderer.material.SetColor("_DissolveColor", _color * _intensity);
        _renderer.material.SetFloat("_Fade", currentFade);
    }
}
