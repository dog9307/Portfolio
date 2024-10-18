using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPassiveSkillStorage : SavableObject
{
    private PlayerAttacker _player;

    private List<int> _passiveSkills = new List<int>();
    public List<int> passiveSkills { get { return _passiveSkills; } }

    private PlayerSkillUsage _playerSkill;

    private int _currentPassiveIndex;
    public int currentPassiveIndex { get { return _currentPassiveIndex; } set { _currentPassiveIndex = value; } }

    public int equipedCount
    {
        get
        {
            int count = 0;
            foreach (var passive in passiveSkills)
            {
                if (passive != -1)
                    count++;
            }

            return count;
        }
    }

    void Start()
    {
        LoadPassive();
    }

    void Update()
    {
        if (KeyManager.instance.IsOnceKeyDown("attack_change_1"))
            ChangePassive(0);
        if (KeyManager.instance.IsOnceKeyDown("attack_change_2"))
            ChangePassive(1);
        if (KeyManager.instance.IsOnceKeyDown("attack_change_3"))
            ChangePassive(2);
        if (KeyManager.instance.IsOnceKeyDown("attack_change_4"))
            ChangePassive(3);
    }

    public void AddPassiveInList(int targetIndex, int id)
    {
        for (int i = 0; i < passiveSkills.Count; ++i)
        {
            if (passiveSkills[i] == id)
            {
                passiveSkills[i] = passiveSkills[targetIndex];
                break;
            }
        }

        _passiveSkills[targetIndex] = id;
    }

    public void ChangePassive(int index)
    {
        if (!_player)
            _player = FindObjectOfType<PlayerAttacker>();

        if (_player.isAttacking) return;

        if (!_playerSkill)
            _playerSkill = GetComponent<PlayerSkillUsage>();

        PassiveChargeAttackUser charge = _playerSkill.FindUser<PassiveChargeAttack>() as PassiveChargeAttackUser;
        if (charge)
        {
            if (charge.isChargingStart)
                return;
        }

        _currentPassiveIndex = index;

        int skillId = _passiveSkills[_currentPassiveIndex];

        if (skillId == -1) return;

        SkillBase newSkill = SkillFactory.CreateSkill(skillId);
        _playerSkill.PassiveSkillChange(newSkill);
    }

    public void AddPassive(int passiveID)
    {
        foreach (var skill in passiveSkills)
        {
            if (passiveID == skill)
                return;
        }

        passiveSkills.Add(passiveID);

        if (equipedCount < 4)
        {
            int index = -1;
            for (int i = 0; i < passiveSkills.Count; ++i)
            {
                if (passiveSkills[i] == -1)
                {
                    index = i;
                    break;
                }
            }

            AddPassiveInList(index, passiveID);
        }
    }

    public void SelectSkill(int selectID)
    {
        for (int i = 0; i < passiveSkills.Count; ++i)
        {
            if (passiveSkills[i] == selectID)
            {
                _currentPassiveIndex = i;
                break;
            }
        }
    }

    public bool IsExist(int find)
    {
        bool isExist = false;
        foreach (var id in _passiveSkills)
        {
            if (id == find)
            {
                isExist = true;
                break;
            }
        }

        return isExist;
    }

    public void LoadPassive()
    {
        if (_passiveSkills == null)
            _passiveSkills = new List<int>();

        _passiveSkills.Clear();
        for (int i = 0; i < 4; ++i)
        {
            int id = PlayerPrefs.GetInt(_key + i.ToString(), -1);

            _passiveSkills.Add(id);
        }
    }

    public bool IsPassiveExist(int id)
    {
        for (int i = 0; i < _passiveSkills.Count; ++i)
        {
            if (_passiveSkills[i] == id)
                return true;
        }

        return false;
    }

    public override SavableNode[] GetSaveNodes()
    {
        SavableNode[] nodes = new SavableNode[_passiveSkills.Count];

        for (int i = 0; i < _passiveSkills.Count; ++i)
        {
            nodes[i] = new SavableNode();

            nodes[i].key = _key + i.ToString();
            nodes[i].value = _passiveSkills[i];
        }

        return nodes;
    }
}
