using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleGlowableObject : MonoBehaviour
{
    [SerializeField]
    private bool _isAffectedByLit = false;

    [SerializeField]
    private SHADER_EFFECT _type = SHADER_EFFECT.GlowEffect;
    private static string path = "ShaderEffect";

    private Material _glowMat = null;
    public Material glowMat
    {
        get
        {
            if (!_glowMat)
            {
                string effectName = _type.ToString();
                string matName = effectName + "MultipleMaterial";
                if (_isAffectedByLit)
                    matName += "Lit";

                _glowMat = Instantiate<Material>(Resources.Load<Material>(path + "/" + effectName + "/" + matName));

                _renderer = GetComponent<SpriteRenderer>();
                _renderer.sharedMaterial = Resources.Load<Material>(path + matName);
                _renderer.material = _glowMat;
            }

            return _glowMat;
        }
    }

    [SerializeField]
    private Color _color;
    public Color color { get { return _color; } set { _color = value; } }
    [SerializeField]
    private Color _color2;
    public Color color2 { get { return _color2; } set { _color2 = value; } }
    [SerializeField]
    private Color _color3;
    public Color color3 { get { return _color3; } set { _color3 = value; } }

    [SerializeField]
    private float _intensity;
    [SerializeField]
    private float _intensity2;
    [SerializeField]
    private float _intensity3;

    private SpriteRenderer _renderer;

    // Start is called before the first frame update
    void Awake()
    {
        glowMat.SetColor("_Color1", new Color(_color.r, _color.g, _color.b, 1.0f) * _intensity);
        glowMat.SetColor("_Color2", new Color(_color2.r, _color2.g, _color2.b, 1.0f) * _intensity2);
        glowMat.SetColor("_Color3", new Color(_color3.r, _color3.g, _color3.b, 1.0f) * _intensity3);   
    }
        
    private void Update()
    {
        Texture texture = null;

        texture = glowMat.GetTexture("_Emission2");
        if (texture)
        {
            if (texture.name == glowMat.GetTexture("_MainTex").name + "_Emission2")
            {
                glowMat.SetColor("_Color2", new Color(_color2.r, _color2.g, _color2.b, 1.0f) * _intensity2);
            }
            else glowMat.SetColor("_Color2", new Color(_color2.r, _color2.g, _color2.b, 1.0f) * 0);
        }
        //else if (glowMat.GetTexture("_Emission2"))
        //    glowMat.SetColor("_Color2", new Color(_color2.r, _color2.g, _color2.b, 1.0f) * _intensity2);

        texture = glowMat.GetTexture("_Emission3");
        if (texture)
        {
            if (texture.name == glowMat.GetTexture("_MainTex").name + "_Emission3")
            {
                glowMat.SetColor("_Color3", new Color(_color3.r, _color3.g, _color3.b, 1.0f) * _intensity3);
            }
            else glowMat.SetColor("_Color3", new Color(_color3.r, _color3.g, _color3.b, 1.0f) * 0);
        }

        //if (glowMat.GetTexture("_Emission3") == null)
        //{
        //    glowMat.SetColor("_Color3", new Color(_color3.r, _color3.g, _color3.b, 1.0f) * 0);
        //}
        //else if (glowMat.GetTexture("_Emission3"))
        //    glowMat.SetColor("_Color3", new Color(_color3.r, _color3.g, _color3.b, 1.0f) * _intensity3);
        //else if (glowMat.GetTexture("_Emission3").name == glowMat.mainTexture.name + "_emission")
        //    glowMat.SetColor("_Color3", new Color(_color3.r, _color3.g, _color3.b, 1.0f) * _intensity3);
    }

}