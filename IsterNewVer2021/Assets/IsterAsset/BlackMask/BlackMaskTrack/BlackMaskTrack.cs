using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.3f, 0.3f, 0.3f)]
[TrackClipType(typeof(BlackMaskClip))]
public class BlackMaskTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        var scriptPlayable = ScriptPlayable<BlackMaskMixerBehaviour>.Create(graph, inputCount);
        return scriptPlayable;
    }
}
