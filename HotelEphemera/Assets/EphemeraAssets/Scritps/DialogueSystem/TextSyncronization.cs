using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSyncronization : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _normal;
    [SerializeField]
    private TextMeshProUGUI _highlighted;

    void Update()
    {
        _highlighted.text = _normal.text;
    }
}
