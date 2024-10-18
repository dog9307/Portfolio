using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveIn : MonoBehaviour
{
    [SerializeField]
    private Color _color;
    [SerializeField]
    private float _intensity;
    [SerializeField]
    private float _scale = 10.0f;

    [SerializeField]
    private float _totalTime;
    private float _currentTime;

    //[SerializeField]
    //private MonoBehaviour[] _nextComponents;
    private float _currentFade;
    [HideInInspector]
    public bool _dissolveOn;

    private SpriteRenderer _renderer;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _currentFade = 0.0f;

        _renderer.material.SetColor("_DissolveColor", _color * _intensity);
        _renderer.material.SetFloat("_Fade", _currentFade);
    }

    void Update()
    {
        _currentTime += IsterTimeManager.deltaTime;
        _currentFade = _currentTime / _totalTime;
        if (_currentFade >= 0.9999f)
        {
            _dissolveOn = true;
            //_currentFade = 1.0f;
            AddComponents();
        }

        _currentFade = Mathf.Lerp(0.0f, 1.0f, _currentFade);
        _renderer.material.SetFloat("_Fade", _currentFade);
    }

    void AddComponents()
    {
        //foreach (MonoBehaviour com in _nextComponents)
        //    com.enabled = true;

        //Destroy(this);
    }
}
