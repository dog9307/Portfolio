using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryHelperBase<T> where T : ItemBase
{
    private List<T> _items = new List<T>();
    public List<T> items { get { return _items; } }

    public virtual void AddItem(T newItem)
    {
        newItem.Init();
        _items.Add(newItem);
    }

    public virtual void RemoveItem(int id)
    {
        for (int i = 0; i < _items.Count; ++i)
        {
            if (_items[i].id == id)
            {
                GameObject.FindObjectOfType<RelicIconList>().RelicIconOff(id);

                _items[i].Release();
                _items.RemoveAt(i);
                return;
            }
        }
    }

    public virtual void RemoveItem(T item)
    {
        for (int i = 0; i < _items.Count; ++i)
        {
            if (_items[i] == item)
            {
                GameObject.FindObjectOfType<RelicIconList>().RelicIconOff(item.id);

                _items[i].Release();
                _items.RemoveAt(i);
                return;
            }
        }
    }

    public virtual void RemoveItemAll()
    {
        foreach (var item in _items)
        {
            GameObject.FindObjectOfType<RelicIconList>().RelicIconOff(item.id);
            item.Release();
        }
        _items.Clear();
    }

    public virtual T FindItem(int id)
    {
        T find = default(T);

        for (int i = 0; i < _items.Count; ++i)
        {
            if (_items[i].id == id)
            {
                find = _items[i];
                break;
            }
        }

        return find;
    }

    public virtual List<T> FindItems(int id)
    {
        List<T> findList = new List<T>();

        for (int i = 0; i < _items.Count; ++i)
        {
            if (_items[i].id == id)
                findList.Add(_items[i]);
        }

        return findList;
    }
}
