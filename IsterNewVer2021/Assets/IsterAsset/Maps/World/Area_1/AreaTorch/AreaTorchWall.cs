using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AreaTorchType
{
    NONE = -1,
    Red,
    Yellow,
    Blue,
    END
}

public class AreaTorchWall : MonoBehaviour
{
    [SerializeField]
    private AreaTorchType _type;
    public AreaTorchType type { get { return _type; } }

    [SerializeField]
    private ParticleSystem _effect;

    [SerializeField]
    private Collider2D _col;

    [SerializeField]
    private Collider2D[] _cols;

    [SerializeField]
    private bool _isStartOff = false;

    void OnEnable()
    {
        if (_isStartOff)
        {
            if (_effect)
                _effect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            if (_col)
                _col.enabled = false;

            foreach (var c in _cols)
                c.enabled = false;
        }
    }

    public void TurnOnWall()
    {
        if (_effect)
            _effect.Play();

        if (_col)
            _col.enabled = true;

        foreach (var c in _cols)
            c.enabled = true;
    }

    public void TurnOffWall()
    {
        if (_effect)
            _effect.Stop();

        if (_col)
            _col.enabled = false;

        foreach (var c in _cols)
            c.enabled = false;
    }
}
