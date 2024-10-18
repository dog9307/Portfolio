using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Michsky.UI.Dark;

[System.Serializable]
public class ItemContentBinder : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private Image _itemImage;
    public Sprite itemImage { get { if (_itemImage) return _itemImage.sprite; return null; } set { if (_itemImage) _itemImage.sprite = value; } }

    [HideInInspector]
    [SerializeField]
    private UIDissolveEffect _dissolve;

    public void ItemGain()
    {
        gameObject.SetActive(true);

        _dissolve?.DissolveIn();
    }

    public void ItemLose()
    {
        _dissolve?.DissolveOut();
    }
}
