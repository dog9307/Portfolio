using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayableDictionary : SerializableDictionary<string, GameObject> { };

public class PlayableCharacterManager : MonoBehaviour
{
    [SerializeField]
    private PlayableDictionary _npcs = new PlayableDictionary();

    public void CharacterChange(CharacterInfo prev, CharacterInfo next)
    {
        _npcs[prev.characterName].SetActive(true);
        _npcs[next.characterName].SetActive(false);
    }
}
