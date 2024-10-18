using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowBomb : MonoBehaviour, IObjectCreator
{
    [SerializeField]
    private GameObject _lava;
    public GameObject effectPrefab { get { return _lava; } set { _lava = value; } }

    public bool isLavaCreate { get; set; }

    void OnDestroy()
    {
        if (!isLavaCreate) return;

        CreateObject();
    }

    public GameObject CreateObject()
    {
        GameObject newLava = Instantiate(effectPrefab);
        newLava.transform.position = transform.position;

        return newLava;
    }
}
