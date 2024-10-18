using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSymbolSwitching : MonoBehaviour
{
    [SerializeField]
    private Image _ui;

    [SerializeField]
    private Sprite _sp0;
    [SerializeField]
    private Sprite _sp1;

    [SerializeField]
    private Image _cover;

    [SerializeField]
    private Sprite _spCover0;
    [SerializeField]
    private Sprite _spCover1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                _ui.sprite = _sp0;
                _cover.sprite = _spCover0;
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                _ui.sprite = _sp1;
                _cover.sprite = _spCover1;
            }
        }
    }
}
