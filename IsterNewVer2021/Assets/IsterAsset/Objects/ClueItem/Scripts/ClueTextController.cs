using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueTextController : MonoBehaviour
{
    [SerializeField]
    private int _id = 100;
    public int id { get { return _id; } }

    [HideInInspector]
    [SerializeField]
    private GameObject _seperator;

    public void SeperatorOn(bool isOn)
    {
        _seperator.SetActive(isOn);
    }
}
