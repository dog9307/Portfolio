using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerF1FlowerUIController : MonoBehaviour
{
    [SerializeField]
    private TowerF1FlowerBuffSlot[] _slots;

    void Start()
    {
        foreach (var slot in _slots)
            slot.gameObject.SetActive(false);
    }

    public void UIOn(FlowerType type)
    {
        foreach (var slot in _slots)
        {
            if (slot.type == type)
            {
                slot.gameObject.SetActive(true);
                break;
            }
        }
    }

    public void UIOff(FlowerType type)
    {
        foreach (var slot in _slots)
        {
            if (slot.type == type)
            {
                slot.gameObject.SetActive(false);
                break;
            }
        }
    }
}
