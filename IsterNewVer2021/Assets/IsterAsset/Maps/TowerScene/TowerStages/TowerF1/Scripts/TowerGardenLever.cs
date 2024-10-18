using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGardenLever : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _walls;

    public void LeverActivate()
    {
        foreach (var w in _walls)
            w.SetActive(!w.activeSelf);
    }
}
