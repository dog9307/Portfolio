using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmbientController : MonoBehaviour
{
    [SerializeField]
    private float _globalLightIntensity = 1.0f;
    public float globalLightIntensity { get { return _globalLightIntensity; } }

    [SerializeField]
    private Color _cameraSolidColor;
    public Color cameraSolidColor { get { return _cameraSolidColor; } }

    [SerializeField]
    private float _fallingDamage = 10.0f;
    public float fallingDamage { get { return _fallingDamage; } set { _fallingDamage = value; } }

    private UnityEngine.Rendering.Universal.Light2D _globalLight;

    [SerializeField]
    private bool _isResetable = true;

    public void AmbientReset()
    {
        if (!_isResetable) return;

        if (!_globalLight)
            _globalLight = FindGlobal();

        if (_globalLight)
            _globalLight.intensity = _globalLightIntensity;

        Camera.main.backgroundColor = _cameraSolidColor;

        PlayerFlyingChecker flying =FindObjectOfType<PlayerFlyingChecker>();
        if (flying)
            flying.fallingDamage = _fallingDamage;
    }

    UnityEngine.Rendering.Universal.Light2D FindGlobal()
    {
        UnityEngine.Rendering.Universal.Light2D global = null;

        UnityEngine.Rendering.Universal.Light2D[] lights = FindObjectsOfType<UnityEngine.Rendering.Universal.Light2D>();
        foreach (var light in lights)
        {
            if (light.lightType == UnityEngine.Rendering.Universal.Light2D.LightType.Global)
            {
                global = light;
                break;
            }
        }

        return global;
    }

    public void SetAmbient(float global, Color cameraColor)
    {
        if (!_globalLight)
            _globalLight = FindGlobal();

        if (_globalLight)
            _globalLight.intensity = global;

        Camera.main.backgroundColor = cameraColor;
    }
}
