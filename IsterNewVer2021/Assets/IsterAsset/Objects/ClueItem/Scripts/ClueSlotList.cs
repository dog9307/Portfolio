using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueSlotList : MonoBehaviour
{
    [SerializeField]
    private ClueButtonController[] _slots;

    [SerializeField]
    private ClueTextListManager _manager;

    void Start()
    {
        foreach (var b in _slots)
        {
            string key = "Clue_" + b.keyward;
            int count = PlayerPrefs.GetInt(key, 0);
            if (count >= 100)
                b.OpenButton();
        }

        if (!_manager)
            _manager = FindObjectOfType<ClueTextListManager>();

        _manager.Load();
    }

    //IEnumerator FindInventory()
    //{
    //    PlayerInventory inventory = FindObjectOfType<PlayerInventory>();

    //    while (!inventory)
    //    {
    //        yield return null;

    //        inventory = FindObjectOfType<PlayerInventory>();
    //    }

    //    int count = PlayerPrefs.GetInt("RelicCount", 0);

    //    for (int i = 0; i < count; ++i)
    //    {
    //        string key = "relic_" + i.ToString();
    //        int id = PlayerPrefs.GetInt(key, -1);

    //        if (id >= 100)
    //        {
    //            RelicItemBase item = RelicItemFactory.CreateItem(id);
    //            inventory.AddRelicItem(item);
    //        }
    //    }
    //}

    public void ButtonOn(string keyward, int id)
    {
        foreach (var slot in _slots)
        {
            if (slot.keyward == keyward)
            {
                slot.GainClue(id);
                break;
            }
        }
    }

    //public void ButtonOff(string keyward)
    //{
    //    foreach (var slot in _slots)
    //    {
    //        if (slot.id == id)
    //            slot.gameObject.SetActive(false);
    //    }
    //}
}
