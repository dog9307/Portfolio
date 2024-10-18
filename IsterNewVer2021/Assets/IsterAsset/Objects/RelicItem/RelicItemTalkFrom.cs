using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicItemTalkFrom : ItemTalkFrom
{
    [SerializeField]
    private DisposableObject _disposable;

    //public override void Talk(PlayerInfo currentPlayer)
    public override void Talk()
    {
        if (!_inventory)
            _inventory = FindObjectOfType<PlayerInventory>();

        RelicItemBase item = _inventory.AddRelicItem(this);
        if (item == null)
            return;

        item.UseItem();

        PlaySFX();

        if (_isDestroy)
            Destroy(gameObject);

        SavableObject save = GetComponent<SavableObject>();
        if (save)
            save.AddSaveData();

        if (_disposable)
            _disposable.UseObject();
    }
}
