using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class BlackMaskClip : PlayableAsset, ITimelineClipAsset
{
    [HideInInspector]
    public BlackMaskBehaviour template = new BlackMaskBehaviour();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Extrapolation | ClipCaps.Blending; }
    }

    [Range(0.0f, 1.0f)] public float targetAlpha = 1.0f;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<BlackMaskBehaviour>.Create(graph, template);
        BlackMaskBehaviour clone = playable.GetBehaviour();

        clone.targetAlpha = targetAlpha;

        return playable;
    }
}
