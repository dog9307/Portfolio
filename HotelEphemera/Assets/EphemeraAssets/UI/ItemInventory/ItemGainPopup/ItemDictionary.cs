using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemSpriteInfo
{
    public Sprite sprite;
    public ItemContentBinder binder;
}

[System.Serializable]
public class ItemDictionary : SerializableDictionary<int, ItemSpriteInfo> { };
