using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfoSignal : MonoBehaviour
{
    [SerializeField]
    private string _mapName;

    [SerializeField]
    private Sprite _infoBack;

    [SerializeField]
    private bool _isEnableShow = true;
    public bool isEnableShow { get { return _isEnableShow; } set { _isEnableShow = value; } }

    void OnEnable()
    {
        if (!_isEnableShow) return;

        Signal();
    }

    public void Signal()
    {
        MapInfo info = FindObjectOfType<MapInfo>();
        if (info)
            info.ShowMapInfo(_infoBack, _mapName);
    }
}

[System.Serializable]
public class MapLocalizedInfoDictionary : SerializableDictionary<string, string> { };
