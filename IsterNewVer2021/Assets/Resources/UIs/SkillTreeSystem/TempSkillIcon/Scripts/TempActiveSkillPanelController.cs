using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempActiveSkillPanelController : MonoBehaviour
{
    [SerializeField]
    private List<TempSkillSlot> _skillSlots = new List<TempSkillSlot>();

    private PlayerSkillUsage _playerSkill;
    private PlayerAttacker _playerAttack;

    private HAND _prevHand;
    private HAND _currentHand;

    void Start()
    {
        ChangeIcons();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_playerSkill)
            _playerSkill = FindObjectOfType<PlayerFindHelper>().player.GetComponent<PlayerSkillUsage>();
        if (!_playerAttack)
            _playerAttack = FindObjectOfType<PlayerFindHelper>().player.GetComponent<PlayerAttacker>();

        _currentHand = _playerAttack.currentHand;
        if (_prevHand != _currentHand)
        {
            ChangeIcons();
            _prevHand = _currentHand;
        }
    }

    private SkillRotationUIController _rotUI;
    public void ChangeIcons()
    {
        if (!_playerSkill)
            _playerSkill = FindObjectOfType<PlayerFindHelper>().player.GetComponent<PlayerSkillUsage>();
        if (!_playerAttack)
            _playerAttack = FindObjectOfType<PlayerFindHelper>().player.GetComponent<PlayerAttacker>();

        int startIndex = (int)_currentHand * PlayerSkillUsage.HAND_MAX_COUNT;
        for (int i = 0; i < _playerSkill.activeSkills.Count; ++i)
        {
            SkillBase skill = null;
            if (startIndex + i < _playerSkill.activeSkills.Count)
                skill = _playerSkill.activeSkills[startIndex + i];

            _skillSlots[i].gameObject.SetActive(true);
            _skillSlots[i].ChangeIcon(skill, _playerSkill);
        }

        SkillBase currentSkill = _playerSkill.currentSelectSkill;
        UpdateUI(currentSkill);

        if (!_rotUI)
            _rotUI = FindObjectOfType<SkillRotationUIController>();
        if (_rotUI)
            _rotUI.ChangeIcons(_skillSlots);
    }

    private float _selectedScale = 1.3f;
    private float _normalScale = 1.0f;
    public void UpdateUI(SkillBase currentSelectedSkill)
    {
        foreach (var slot in _skillSlots)
        {
            if (currentSelectedSkill == null)
            {
                slot.ScaleChange(_normalScale);
                continue;
            }

            if (slot.relativeSkill == null)
                slot.ScaleChange(_normalScale);
            else
            {
                if (slot.relativeSkill == currentSelectedSkill)
                    slot.ScaleChange(_selectedScale);
                else
                    slot.ScaleChange(_normalScale);
            }
        }
    }
}
