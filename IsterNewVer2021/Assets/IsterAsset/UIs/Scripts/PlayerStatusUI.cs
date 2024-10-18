using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatusUI : MonoBehaviour
{
    [SerializeField]
    private IntHPPanelController _hpUi;
    private Damagable _player;
    [SerializeField]
    private TextMeshProUGUI _hpText;

    private PlayerInventory _playerInventory;
    [SerializeField]
    private TextMeshProUGUI _darkLightText;


    private void OnEnable()
    {
        if (!_hpUi)
            _hpUi = FindObjectOfType<IntHPPanelController>();

        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>().GetComponent<Damagable>();

        if (_hpUi && _player && _hpText)
            _hpText.text = $"x {_player.totalHP / _hpUi.eachSlotFigure}";

        if (!_playerInventory)
            _playerInventory = FindObjectOfType<PlayerInventory>();

        if (_playerInventory)
            _darkLightText.text = $"{_playerInventory.darkLight}";
    }
}
