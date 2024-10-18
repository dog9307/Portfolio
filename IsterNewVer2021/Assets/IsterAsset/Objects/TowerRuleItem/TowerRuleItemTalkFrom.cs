using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerRuleItemTalkFrom : ItemTalkFrom
{
    //public override void Talk(PlayerInfo currentPlayer)
    public override void Talk()
    {
        if (!_inventory)
            _inventory = FindObjectOfType<PlayerInventory>();

        _inventory.AddRuleItem(this);

        if (_sfx)
            _sfx.PlaySFX(_sfxName);

        Destroy(gameObject);
    }
}
