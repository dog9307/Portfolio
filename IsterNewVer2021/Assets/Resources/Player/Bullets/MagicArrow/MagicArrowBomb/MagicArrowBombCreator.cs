using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowBombCreator : MonoBehaviour, IObjectCreator
{
    [SerializeField]
    private GameObject _prefab;
    public GameObject effectPrefab { get { return _prefab; } set { _prefab = value; } }

    public bool isLavaCreate { get; set; }

    public GameObject CreateObject()
    {
        if (!_prefab)
            return null;

        GameObject newBomb = Instantiate(effectPrefab);
        newBomb.transform.position = transform.position;

        MagicArrowBomb bomb = newBomb.GetComponent<MagicArrowBomb>();
        bomb.isLavaCreate = isLavaCreate;

        return newBomb;
    }
}
