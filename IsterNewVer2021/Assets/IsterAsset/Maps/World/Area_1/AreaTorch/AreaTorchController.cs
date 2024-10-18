using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTorchController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _fire;

    public void TorchColorChange(ParticleSystem.MinMaxGradient color)
    {
        ParticleSystem.ColorOverLifetimeModule col = _fire.colorOverLifetime;
        col.color = color;
    }
}
