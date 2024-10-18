using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PortraitManager : SerializableDictionary<string, PortraitDictionary> { };

[System.Serializable]
public struct PortraitInfo
{
    public Sprite sprite;
    //public Color color;
}

[System.Serializable]
public class PortraitDictionary : SerializableDictionary<string, PortraitInfo> { };
