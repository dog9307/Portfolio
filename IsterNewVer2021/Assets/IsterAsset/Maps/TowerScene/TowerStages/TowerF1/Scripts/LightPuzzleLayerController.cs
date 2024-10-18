using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPuzzleLayerController : MonoBehaviour
{
    [SerializeField]
    private FlowerType _targetType;

    [HideInInspector]
    [SerializeField]
    private GameObject _conditionalWall;

    public void LightChange(FlowerType type)
    {
        if (type == _targetType)
            LightSwitchOn();
        else
            LightSwitchOff();
    }

    public void LightSwitchOn()
    {
        if (_conditionalWall)
            _conditionalWall.SetActive(false);
    }

    public void LightSwitchOff()
    {
        if (_conditionalWall)
            _conditionalWall.SetActive(true);
    }
}
