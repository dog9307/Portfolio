using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICameraContoller : MonoBehaviour
{
    private RoomEffectManager _effectManager;

    public void CheckShowChoices()
    {
        DialogueManager.instance.CheckShowChoices();
    }

    public void EffectPlay(string effectName)
    {
        if (!_effectManager)
            _effectManager = FindObjectOfType<RoomEffectManager>();

        _effectManager.EffectPlayOneShot(effectName);
    }

    [SerializeField]
    private ScenePasser _toOutro;
    public void GoToOutro()
    {
        _toOutro?.StartScenePass();
    }
}
