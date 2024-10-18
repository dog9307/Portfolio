using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPassiveSkillPanelController : MonoBehaviour
{
    [SerializeField]
    private GameObject _slotPrefab;

    private List<TempSkillSlot> _skillSlots = new List<TempSkillSlot>();

    private PlayerSkillUsage _playerSkill;
    private PlayerAttacker _playerAttack;

    public void ChangeIcons()
    {
        if (!_playerSkill)
            _playerSkill = FindObjectOfType<PlayerSkillUsage>();
        if (!_playerAttack)
            _playerAttack = FindObjectOfType<PlayerAttacker>();

        for (int i = 0; i < _playerSkill.passiveSkills.Count; ++i)
        {
            TempSkillSlot slot = null;
            if (i >= _skillSlots.Count)
            {
                GameObject newSlot = Instantiate(_slotPrefab);
                newSlot.transform.parent = transform;
                newSlot.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                slot = newSlot.GetComponent<TempSkillSlot>();

                _skillSlots.Add(slot);
            }
            else
                slot = _skillSlots[i];

            if (!slot) continue;

            SkillBase skill = _playerSkill.passiveSkills[i];
            slot.ChangeIcon(skill, _playerSkill);
        }
    }
}
