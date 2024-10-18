using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicIconController : MonoBehaviour
{
    [SerializeField]
    private int _id;
    public int id { get { return _id; } }

    [SerializeField]
    private Image _icon;
    public Image icon { get { return _icon; } }
}
