using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDummyTalkFrom : NpcTalkFrom   
{
    [SerializeField]
    private GameObject _items;
    [SerializeField]
    private GameObject _healItem;

    public override void OnTalkEnd()
    {
        if (onTalkEnd != null)
            onTalkEnd.Invoke();

        PlayerMoveController player = FindObjectOfType<PlayerMoveController>();
        if (player)
            player.PlayerMoveFreeze(false);

        if (_items)
        {
            _items.SetActive(true);

            Damagable playerHP = player.GetComponent<Damagable>();
            if (playerHP)
            {
                _healItem.SetActive((playerHP.currentHP < playerHP.totalHP));
            }
        }
    }
}
