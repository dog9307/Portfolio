using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DarkLightUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;

    public void UpdateUI(int darkLight)
    {
        _text.text = $"{darkLight}";
    }
}
