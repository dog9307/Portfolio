using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkillEquipedSkillIconManager : MonoBehaviour
{
    [SerializeField]
    private PassiveSkillSelector[] _passives;

    [SerializeField]
    private TempSkillIcon[] _equiedSlots;

    [SerializeField]
    private DragableIconController _dragController;

    private PlayerPassiveSkillStorage _passiveStorage;

    public void Start()
    {
        ChangeIcons();
    }

    public void ChangeIcons()
    {
        if (!_passiveStorage)
            _passiveStorage = FindObjectOfType<PlayerPassiveSkillStorage>();

        List<int> passiveList = _passiveStorage.passiveSkills;
        for (int i = 0; i < _equiedSlots.Length; ++i)
        {
            TempSkillIcon sour = FindPassive(passiveList[i]);

            _equiedSlots[i].SetIcon(sour);
        }
    }

    private TempSkillIcon FindPassive(int id)
    {
        TempSkillIcon icon = null;
        foreach (var p in _passives)
        {
            if (p.skillId == id)
            {
                icon = p.GetComponent<TempSkillIcon>();
                break;
            }
        }

        return icon;
    }

    public void ChangeIcon()
    {
        if (!_passiveStorage)
            _passiveStorage = FindObjectOfType<PlayerPassiveSkillStorage>();

        DragableIcon currentDragable = _dragController.currentIcon;
        TempSkillIcon currentIcon = currentDragable.GetComponent<TempSkillIcon>();
        PassiveSkillSelector currentSelector = currentDragable.GetComponent<PassiveSkillSelector>();

        _passiveStorage.AddPassiveInList(currentDragable.targetIndex, currentSelector.skillId);

        ChangeIcons();
    }
}
