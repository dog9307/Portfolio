using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRewardCondition : MonoBehaviour
{
    [SerializeField]
    private string _key = "";

    [SerializeField]
    private int _afterValue = 100;

    [SerializeField]
    private GameObject _newReward;
    public GameObject newReward { get { return _newReward; } }

    public bool IsChangeDropItem()
    {
        return (PlayerPrefs.GetInt(_key, -1) >= _afterValue);
    }
}
