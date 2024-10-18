using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicIconList : SavableObject
{
    [SerializeField]
    private RelicIconController[] _slots;

    void Start()
    {
        StartCoroutine(FindInventory());
    }

    IEnumerator FindInventory()
    {
        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();

        while (!inventory)
        {
            yield return null;

            inventory = FindObjectOfType<PlayerInventory>();
        }

        int count = PlayerPrefs.GetInt("RelicCount", 0);

        for (int i = 0; i < count; ++i)
        {
            string key = "relic_" + i.ToString();
            int id = PlayerPrefs.GetInt(key, -1);

            if (id >= 100)
            {
                RelicItemBase item = RelicItemFactory.CreateItem(id);
                inventory.AddRelicItem(item);
            }
        }
    }

    public void RelicIconOn(int id)
    {
        foreach (var slot in _slots)
        {
            if (slot.id == id)
            {
                Transform prevParent = slot.transform.parent;
                slot.transform.parent = null;
                slot.transform.parent = prevParent;
                slot.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                slot.gameObject.SetActive(true);
            }
        }
    }

    public void RelicIconOff(int id)
    {
        foreach (var slot in _slots)
        {
            if (slot.id == id)
                slot.gameObject.SetActive(false);
        }
    }

    public override SavableNode[] GetSaveNodes()
    {
        List<SavableNode> saves = new List<SavableNode>();
        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
        if (inventory)
        {
            List<RelicItemBase> relics = inventory.relicInventory.items;
            int count = relics.Count;
            SavableNode save = new SavableNode();
            save.key = "RelicCount";
            save.value = count;
            saves.Add(save);

            for (int i = 0; i < relics.Count; ++i)
            {
                save = new SavableNode();

                string key = "relic_" + i.ToString();
                int id = relics[i].id;

                save.key = key;
                save.value = id;

                saves.Add(save);
            }
        }

        return saves.ToArray();
    }
}
