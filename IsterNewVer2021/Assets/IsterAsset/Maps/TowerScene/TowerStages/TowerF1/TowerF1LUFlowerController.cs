using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerF1LUFlowerController : MonoBehaviour
{
    private GameObject _child;

    public void BattleStart()
    {
        Transform child = transform.GetChild(0);
        if (child)
        {
            _child = child.gameObject;
            _child.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void BattleEnd()
    {
        if (_child)
            _child.transform.GetChild(0).gameObject.SetActive(true);
    }
}
