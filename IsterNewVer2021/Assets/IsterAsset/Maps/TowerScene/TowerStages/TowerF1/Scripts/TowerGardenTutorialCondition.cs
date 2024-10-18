using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGardenTutorialCondition : ColliderTriggerCondition
{
    [SerializeField]
    private string _key = "LiatrisTutorial";
    [SerializeField]
    private int _afterValue = 100;

    public override bool IsCanTrigger()
    {
        bool isCanTrigger = PlayerPrefs.GetInt(_key, 0) < _afterValue;
        return isCanTrigger;
    }

    public void TriggerTodo()
    {
        PlayerPrefs.SetInt(_key, _afterValue);
    }
}
