﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(StringStringDictionary))]
[CustomPropertyDrawer(typeof(ObjectColorDictionary))]
[CustomPropertyDrawer(typeof(StringColorArrayDictionary))]
[CustomPropertyDrawer(typeof(PortraitDictionary))]
[CustomPropertyDrawer(typeof(PortraitManager))]
[CustomPropertyDrawer(typeof(ItemDictionary))]
[CustomPropertyDrawer(typeof(RoomEffectDictionary))]
[CustomPropertyDrawer(typeof(SFXDictionary))]
//[CustomPropertyDrawer(typeof(ItemManager))]
[CustomPropertyDrawer(typeof(KeyDictionary))]
public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}

[CustomPropertyDrawer(typeof(ColorArrayStorage))]
public class AnySerializableDictionaryStoragePropertyDrawer: SerializableDictionaryStoragePropertyDrawer {}
