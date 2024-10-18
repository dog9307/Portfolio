using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HugeCrackCondition : MonoBehaviour
{
    public bool IsCrackOpen()
    {
        int count = PlayerPrefs.GetInt("TowerGardenIn", -1);
        return (count >= 100);
    }
}
