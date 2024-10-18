using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleColorChanger : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] _particles;

    public void ColorChange(Color color)
    {
        foreach (var p in _particles)
        {
            ParticleSystem.MainModule main = p.main;
            ParticleSystem.MinMaxGradient gra = main.startColor;

            Color newColor = color;
            newColor.a = gra.color.a;
            gra.color = newColor;

            main.startColor = gra;
        }
    }

}
