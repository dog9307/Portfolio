using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerGroupChanger : MonoBehaviour
{
    [SerializeField]
    private AudioMixerGroup _group;
    [SerializeField]
    private SoundType _targetType;

    public void ChangeGroup()
    {
        if (!gameObject.activeInHierarchy) return;
        if (!SoundSystem.instance) return;

        SoundSystem.instance.ChangeMixerGroup(_group, _targetType);
    }
}
