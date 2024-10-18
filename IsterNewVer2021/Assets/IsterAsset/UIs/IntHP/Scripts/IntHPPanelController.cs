using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntHPPanelController : MonoBehaviour
{
    [SerializeField]
    private IntHPNode[] _slots;

    [SerializeField]
    private float _eachSlotFigure = 100.0f;
    public float eachSlotFigure { get { return _eachSlotFigure; } }

    private Damagable _playerHP;

    [SerializeField]
    private Image _background;

    [SerializeField]
    private Color _normalHPColor = Color.white;
    [SerializeField]
    private Color _extraHPColor = Color.cyan;

    void Start()
    {
        PlayerHPUIController uiCon = FindObjectOfType<PlayerHPUIController>();
        if (uiCon)
            uiCon.UIAppear();
    }

    void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (!_playerHP)
        {
            _playerHP = FindObjectOfType<PlayerFindHelper>().player.GetComponent<Damagable>();
            if (!_playerHP) return;
        }

        for (int i = 0; i < _slots.Length; ++i)
            _slots[i].gameObject.SetActive(false);

        int totalCount = 0;

        float totalHP = _playerHP.totalHP;
        float currentHP = _playerHP.currentHP;
        totalCount += SlotSetting(totalHP, currentHP, _normalHPColor, 0);

        totalHP = _playerHP.extraTotalHP;
        currentHP = _playerHP.extraCurrentHP;
        totalCount += SlotSetting(totalHP, currentHP, _extraHPColor, totalCount);

        if (_background)
        {
            Vector2 size = _background.rectTransform.sizeDelta;
            size.x = 50.0f + 75.0f * totalCount;
            _background.rectTransform.sizeDelta = size;
        }
    }

    int SlotSetting(float totalHP, float currentHP, Color color, int startIndex)
    {
        int totalCount = (int)(totalHP / _eachSlotFigure);
        int currentCount = (int)(currentHP / _eachSlotFigure);
        for (int i = startIndex; i < startIndex + totalCount; ++i)
        {
            _slots[i].gameObject.SetActive(true);
            if (i < startIndex + currentCount)
                _slots[i].UpdateUI(HPNODE.FULL);
            else if (i == startIndex + currentCount)
            {
                float temp = currentCount * _eachSlotFigure;
                float check = currentHP - temp;
                if (check >= _eachSlotFigure / 2.0f)
                    _slots[i].UpdateUI(HPNODE.HALF);
                else
                    _slots[i].UpdateUI(HPNODE.EMPTY);
            }
            else
                _slots[i].UpdateUI(HPNODE.EMPTY);

            _slots[i].ApplyColor(color);
        }

        return totalCount;
    }
}
