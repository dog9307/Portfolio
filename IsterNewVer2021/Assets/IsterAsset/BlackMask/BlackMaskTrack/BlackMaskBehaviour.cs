using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public class BlackMaskBehaviour : PlayableBehaviour
{
    public BlackMaskController blackMask;

    [Range(0.0f, 1.0f)] public float targetAlpha = 1.0f;

    public static float currentAlpha = 0.0f;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (!blackMask)
            blackMask = BlackMaskController.instance;

        blackMask.ApplyAlpha(currentAlpha);
    }
}
