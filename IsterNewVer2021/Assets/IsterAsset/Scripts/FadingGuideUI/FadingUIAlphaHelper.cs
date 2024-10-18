using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingUIAlphaHelper : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 1.0f)] private float _alphaMultiplier;
    public float alphaMultiplier { get { return _alphaMultiplier; } }
}
