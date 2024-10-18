using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SHADER_EFFECT
{
    GlowEffect,
    Outline,
    OnlyGlow
}

[System.Serializable]
public class GlowableObject : MonoBehaviour
{
    [SerializeField]
    private bool _isAffectedByLit = false;
    [SerializeField]
    private bool _isFillAmountObject = false;

    [SerializeField]
    private SHADER_EFFECT _type = SHADER_EFFECT.GlowEffect;
    private static string path = "ShaderEffect";

    [SerializeField]
    private Material _glowMat;
    public Material glowMat
    {
        get
        {
            if (!_glowMat)
            {
                string effectName = _type.ToString();
                string matName = effectName + "Material";
                if (_isAffectedByLit)
                    matName += "Lit";

                string fullPath = path + "/" + effectName + "/" + matName;
                _glowMat = Instantiate<Material>(Resources.Load<Material>(fullPath));

                _renderer = GetComponent<SpriteRenderer>();
                if (!_renderer)
                {
                    _renderer = GetComponent<ParticleSystemRenderer>();

                    if (!_renderer) return null;
                }
                _renderer.sharedMaterial = Resources.Load<Material>(path + matName);
                _renderer.material = _glowMat;
            }

            return _glowMat;
        }
    }

    [SerializeField]
    private Color _color;
    public Color color { get { return _color; } set { _color = value; } }

    private float _progress = 0;
    [SerializeField]
    private float _intensity;
    [SerializeField]
    public float intensity { get { return _intensity; } set { _intensity = value; } }

    private Renderer _renderer;

    // Start is called before the first frame update
    void Awake()
    {
        glowMat.SetColor("_Color", new Color(_color.r, _color.g, _color.b, 1.0f) * _intensity);
    }
    private void Start()
    {
        if (_isFillAmountObject)
        {
            //glowMat.SetTextureScale("UV0", new Vector2(1.0f, 0));
        }
    }
    private void Update()
    {
        if (_isFillAmountObject)
        {
            if (Input.GetKeyDown(KeyCode.F7))
            {
                FillAmountGauge(5.0f);
                Debug.Log("맞음");
            }
        }
    }

    public void ApplyColor(Color newColor)
    {
        _color = newColor;

        glowMat.SetColor("_Color", new Color(_color.r, _color.g, _color.b, 1.0f) * _intensity);
    }

    public void FillAmountGauge(float _gauge)
    {
        _progress += _gauge/100;
        glowMat.SetTextureOffset("_MainTex", new Vector2(1.0f, _progress));
    }

    public void SetIntensity(float intensity)
    {
        _intensity = intensity;
        glowMat.SetColor("_Color", new Color(_color.r, _color.g, _color.b, 1.0f) * _intensity);
    }

    public void GlowSync(GlowableObject other)
    {
        _intensity = other.intensity;
        _color = other.color;

        glowMat.SetColor("_Color", new Color(_color.r, _color.g, _color.b, 1.0f) * _intensity);
    }
}
