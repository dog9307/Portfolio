using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestShadowController : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.Rendering.Universal.Light2D _light;

    public float maxRange { get { if (_light) return _light.pointLightOuterRadius; return 0.0f; } }

    // Start is called before the first frame update
    void Start()
    {
        if (!_light)
            _light = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    }
}
