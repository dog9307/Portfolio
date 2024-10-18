using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWaterFXHelper : MonoBehaviour
{
    private WaterFX _currentWater;
    [SerializeField]
    private float _yOffset = 0.5f;

    void UpdateYPos()
    {
        if (!_currentWater) return;

        _currentWater.m_yPosition = transform.position.y - _yOffset;
    }

    public void ChangeWater(WaterFX current)
    {
        _currentWater = current;
        current.m_targetCamera = Camera.main;

        UpdateYPos();
    }
}
