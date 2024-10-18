using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRoomReward : MonoBehaviour
{
    [SerializeField]
    private Transform _pos;

    void Start()
    {
        OpenReward();
    }

    public void OpenReward()
    {
        TowerItemRewardManager.instance.OpenReward(_pos);
    }
}
