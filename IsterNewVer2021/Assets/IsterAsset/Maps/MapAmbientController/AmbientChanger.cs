using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientChanger : MonoBehaviour
{
    [SerializeField]
    private float _globalLightIntensity = 1.0f;
    private float _prevLightIntensity = 1.0f;

    [SerializeField]
    private Color _cameraSolidColor;
    private Color _prevCameraSolidColor;

    private static AmbientController _controller;

    public static bool isSkipOnce = false;

    public void ChangeAmbient()
    {
        if (!_controller)
            _controller = FindObjectOfType<AmbientController>();

        _prevLightIntensity = _controller.globalLightIntensity;
        _prevCameraSolidColor = _controller.cameraSolidColor;

        _controller.SetAmbient(_globalLightIntensity, _cameraSolidColor);
    }

    public void ReturnAmbient()
    {
        if (isSkipOnce)
        {
            isSkipOnce = false;
            return;
        }

        if (!_controller)
            _controller = FindObjectOfType<AmbientController>();

        _controller.SetAmbient(_prevLightIntensity, _prevCameraSolidColor);
    }
}
