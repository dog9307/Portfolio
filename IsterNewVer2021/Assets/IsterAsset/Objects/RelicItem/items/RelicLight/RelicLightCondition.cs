using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicLightCondition : ColliderTriggerCondition
{
    private bool _isExistRelicLight;
    private PlayerInventory _inventory;

    public override bool IsCanTrigger()
    {
        return _isExistRelicLight;
    }

    void Update()
    {
        if (_isExistRelicLight) return;

        if (!_inventory)
            _inventory = FindObjectOfType<PlayerInventory>();

        if (!_inventory) return;

        RelicLight light = _inventory.FindRelicItem(100) as RelicLight;
        _isExistRelicLight = (light != null);
    }
}
