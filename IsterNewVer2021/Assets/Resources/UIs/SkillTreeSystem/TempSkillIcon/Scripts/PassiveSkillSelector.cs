using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveSkillSelector : MonoBehaviour
{
    [SerializeField]
    private int _skillId;
    public int skillId { get { return _skillId; } }

    private static PassiveSkillManager manager;
    [SerializeField]
    private GameObject _select;
    [SerializeField]
    private Image _icon;
    public Image icon { get { return _icon; } }

    void Start()
    {
        if (!manager)
            manager = FindObjectOfType<PassiveSkillManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), Input.mousePosition))
                SetSkill();
        }
    }

    public void SetSkill()
    {
        PlayerSkillUsage skill = FindObjectOfType<PlayerSkillUsage>();
        if (skill)
        {
            SkillBase newSkill = SkillFactory.CreateSkill(_skillId);
            skill.PassiveSkillChange(newSkill);

            PlayerPassiveSkillStorage storage = skill.GetComponent<PlayerPassiveSkillStorage>();
            if (storage)
                storage.SelectSkill(_skillId);

            if (!manager)
                manager = FindObjectOfType<PassiveSkillManager>();

            manager.Select(this);
        }
    }

    public void Select()
    {
        _select.SetActive(true);
    }
    public void Deselect()
    {
        _select.SetActive(false);
    }
}
