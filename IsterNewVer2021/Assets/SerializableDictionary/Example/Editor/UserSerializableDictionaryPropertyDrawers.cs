using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(StringStringDictionary))]
[CustomPropertyDrawer(typeof(ObjectColorDictionary))]
[CustomPropertyDrawer(typeof(StringColorArrayDictionary))]
[CustomPropertyDrawer(typeof(KeyDictionary))]
[CustomPropertyDrawer(typeof(PlayableDictionary))]
[CustomPropertyDrawer(typeof(PortraitDictionary))]
[CustomPropertyDrawer(typeof(PortraitManager))]
[CustomPropertyDrawer(typeof(HitInfoDictionary))]
[CustomPropertyDrawer(typeof(EndingIllustDictionary))]
//[CustomPropertyDrawer(typeof(EnemyInStageDictionary))]
//[CustomPropertyDrawer(typeof(EnemyDictionary))]
[CustomPropertyDrawer(typeof(SFXDictionary))]
[CustomPropertyDrawer(typeof(MapLocalizedInfoDictionary))]
public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}

[CustomPropertyDrawer(typeof(ColorArrayStorage))]
public class AnySerializableDictionaryStoragePropertyDrawer: SerializableDictionaryStoragePropertyDrawer {}
