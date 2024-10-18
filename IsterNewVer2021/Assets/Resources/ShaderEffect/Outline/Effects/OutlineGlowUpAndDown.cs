using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineGlowUpAndDown : GlowUpAndDown
{
    [SerializeField]
    private float _thickness = 0.05f;

    // Update is called once per frame
    protected override void Start()
    {
        base.Start();

        glowMat.SetFloat("_Thickness", _thickness);
    }
}
