using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkillInfo))]
public class PlayerSkillUsage : SavableObject
{
    [SerializeField]
    private SkillInfo _skillInfo;
    public List<SkillBase> activeSkills { get { return _skillInfo.activeSkills; } }
    public List<SkillBase> passiveSkills { get { return _skillInfo.passiveSkills; } }

    private PlayerAttacker _attack;
    private Damagable _damagable;

    private bool _isSkillUsing;
    public bool isSkillUsing { get { return _isSkillUsing; } set { _isSkillUsing = value; } }

    public bool isPassiveReversePowerOn { get; set; }
    public bool isActiveReversePowerOn { get; set; }

    public readonly static int HAND_MAX_COUNT = 4;
    public int maxCount
    {
        get
        {
            DualSword dual = GetComponent<DualSword>();
            if (dual)
                return HAND_MAX_COUNT * 2;

            return HAND_MAX_COUNT;
        }
    }

    [SerializeField]
    private SFXPlayer _sfx;

    private ActiveDash _dash;

    //[SerializeField]
    //private int _totalPassiveCost;
    //public int totalPassiveCost { get { return _totalPassiveCost; } set { _totalPassiveCost = value; } }
    //private int _currentPassiveCost;
    //public int currentPassiveCost { get { return _currentPassiveCost; } set { _currentPassiveCost = value; } }

    public static Vector3 useMousePointToWorld { get; set; }

    public PassiveChargeAttackUser chargeUser { get; set; }

    //[SerializeField]
    //private TestPlayerEquipment _playerEquip;

    void Start()
    {
        _attack = GetComponent<PlayerAttacker>();
        _damagable = GetComponent<Damagable>();

        _isSkillUsing = false;

        isPassiveReversePowerOn = isActiveReversePowerOn = false;

        // after skill rotation
        _currentSkillIndex = 0;

        //SkillBase temp = null;
        //activeSkills.Add(temp);
        //activeSkills.Add(temp);
        //activeSkills.Add(temp);
        //activeSkills.Add(temp);

        StartCoroutine(LoadSkill());

        int dash = PlayerPrefs.GetInt("Tuto_dash", 0);
        if (dash >= 100)
            ActiveDashGain();

        // ActiveRangeAttack    - PassiveOtherAttack
        // MagicArrow           - TracingArrow
        // ActiveMirage         - PassiveMirage
        // ActiveDash           - AdditionalAttack
        // ActiveTimeSlow       - PassiveCounterAttack
        // ActiveMarker         - PassiveMarker
        // ActiveCoolTimeDown   - PassiveCoolTimeDown
        // ActiveShield         - PassiveRemover
        // ActiveGravity        - PassiveKnockbackIncrease
        // ActiveItemReroll     - PassiveFortune    
        // ActiveParrying       - PassiveChargeAttack

            // test
            //SkillBase temp;

            //// Left Hand
            //temp = new MagicArrow();
            //_skillInfo.AddSkill(temp);

            //temp = new ActiveMirage();
            //_skillInfo.AddSkill(temp);


            //temp = new TracingArrow();
            //_skillInfo.AddSkill(temp);

            //temp = new PassiveOtherAttack();
            //_skillInfo.AddSkill(temp);

            //temp = new PassiveKnockbackIncrease();
            //_skillInfo.AddSkill(temp);

            //temp = new PassiveCounterAttack();
            //_skillInfo.AddSkill(temp);

            // Right Hand
    }

    IEnumerator LoadSkill()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < activeSkills.Count; ++i)
        {
            string key = _key + i.ToString();
            int id = PlayerPrefs.GetInt(key, 0);
            if (id != 0)
            {
                SkillBase skill = SkillFactory.CreateSkill(id);
                ActiveSkillChange(skill, i);

                if (!_skillSlotPanel)
                    _skillSlotPanel = FindObjectOfType<TempActiveSkillPanelController>();

                if (_skillSlotPanel)
                    _skillSlotPanel.ChangeIcons();
            }

            yield return new WaitForEndOfFrame();
        }
    }

    public void ActiveDashGain()
    {
        SavableNode node = new SavableNode();
        node.key = "Tuto_dash";
        node.value = 100;

        SavableDataManager.instance.AddSavableObject(node);

        _dash = SkillFactory.CreateSkill(100 + (int)ACTIVE.DASH) as ActiveDash;
        _dash.owner = _skillInfo;
        _dash.Init();

        _skillInfo.ActiveDashGain();
    }

    public override SavableNode[] GetSaveNodes()
    {
        SavableNode[] nodes = new SavableNode[activeSkills.Count];
        for (int i = 0; i < activeSkills.Count; ++i)
        {
            nodes[i] = new SavableNode();

            SkillBase active = activeSkills[i];
            nodes[i].key = _key + i.ToString();
            nodes[i].value = 0;
            if (active != null)
                nodes[i].value = active.id;
        }

        return nodes;
    }

    void Update()
    {
        // before skill rotation
        //SelectSkill();

        // after skill rotation
        SelectSkill();
        UseSkill();

        if (KeyManager.instance.IsOnceKeyDown("dash"))
            UseDash();
    }

    public void UseDash()
    {
        if (!IsCanUseSkill()) return;
        if (_dash == null) return;

        if (chargeUser)
        {
            if (chargeUser.isChargingStart)
                return;
        }

        ActiveUserBase user = FindUser(_dash.GetType()) as ActiveUserBase;
        if (!user) return;

        if (user.isCanUseSkill)
            TriggerringSkill(_dash);
    }

    public void SelectSkill(bool isIgnoreKeyManager = false)
    {
        if (activeSkills.Count <= 0) return;

        if (!_rotUI)
            _rotUI = FindObjectOfType<SkillRotationUIController>();
        if (!_skillSlotPanel)
            _skillSlotPanel = FindObjectOfType<TempActiveSkillPanelController>();

        // after skill rotation
        int prev = _currentSkillIndex;
        SkillRotUIDir rotDir = SkillRotUIDir.NONE;
        if (KeyManager.instance.IsOnceKeyDown("skill_rot_left") || isIgnoreKeyManager)
            //||
            //Input.mouseScrollDelta.y > 0.0f)
        {
            int currentIndex = _currentSkillIndex;
            int rotCount = 0;
            int maximumRotCount = activeSkills.Count;
            do
            {
                rotCount++;
                currentIndex = (currentIndex + activeSkills.Count - 1) % activeSkills.Count;
            } while (activeSkills[currentIndex] == null && rotCount < maximumRotCount);

            if (activeSkills[currentIndex] != null)
                _currentSkillIndex = currentIndex;

            //_currentSkillIndex = (_currentSkillIndex + HAND_MAX_COUNT - 1) % HAND_MAX_COUNT;

            //if (_currentSkillIndex >= activeSkills.Count)
            //    _currentSkillIndex = activeSkills.Count - 1;

            rotDir = SkillRotUIDir.LEFT;
        }

        if (KeyManager.instance.IsOnceKeyDown("skill_rot_right") || isIgnoreKeyManager)
            //||
            //Input.mouseScrollDelta.y < 0.0f)
        {
            int currentIndex = _currentSkillIndex;
            int rotCount = 0;
            int maximumRotCount = activeSkills.Count;
            do
            {
                rotCount++;
                currentIndex = (currentIndex + 1) % activeSkills.Count;
            } while (activeSkills[currentIndex] == null && rotCount < maximumRotCount);

            if (activeSkills[currentIndex] != null)
                _currentSkillIndex = currentIndex;

            //_currentSkillIndex = (_currentSkillIndex + 1) % HAND_MAX_COUNT;

            //if (_currentSkillIndex >= activeSkills.Count)
            //    _currentSkillIndex = 0;

            rotDir = SkillRotUIDir.RIGHT;
        }

        if (prev != _currentSkillIndex)
        {
            //SwordChange((int)((ActiveBase)currentSelectSkill).swordType);

            if (_rotUI)
                _rotUI.UpdateUI(rotDir);
            if (_skillSlotPanel)
                _skillSlotPanel.UpdateUI(currentSelectSkill);
        }

        if (_currentSkillIndex < 0)
            _currentSkillIndex = 0;

        //if (prev != _currentSkillIndex)
        //{
        //    //SwordChange((int)((ActiveBase)currentSelectSkill).swordType);

        //    if (_rotUI)
        //        _rotUI.UpdateUI(SkillRotUIDir.RIGHT);
        //    if (_skillSlotPanel)
        //        _skillSlotPanel.UpdateUI(currentSelectSkill);
        //}

        //if (_currentSkillIndex < 0)
        //    _currentSkillIndex = 0;

        // before skill rotation
        //if (KeyManager.instance.IsOnceKeyDown("skill1"))
        //    UseSkill(0 + (int)_attack.currentHand * HAND_MAX_COUNT);
        //if (KeyManager.instance.IsOnceKeyDown("skill2"))
        //    UseSkill(1 + (int)_attack.currentHand * HAND_MAX_COUNT);
        //if (KeyManager.instance.IsOnceKeyDown("skill3"))
        //    UseSkill(2 + (int)_attack.currentHand * HAND_MAX_COUNT);
        //if (KeyManager.instance.IsOnceKeyDown("skill4"))
        //    UseSkill(3 + (int)_attack.currentHand * HAND_MAX_COUNT);
    }

    // after skill rotation
    private int _currentSkillIndex;
    public int currentSkillIndex { get { return _currentSkillIndex; } }
    private SkillRotationUIController _rotUI;
    public SkillBase currentSelectSkill
    {
        get
        {
            if (!_attack)
                return null;

            if (activeSkills.Count <= 0)
                return null;

            if (_currentSkillIndex + (int)_attack.currentHand * HAND_MAX_COUNT >= activeSkills.Count)
                return null;

            return activeSkills[_currentSkillIndex + (int)_attack.currentHand * HAND_MAX_COUNT];
        }
    }
    private TempActiveSkillPanelController _skillSlotPanel;
    private void UseSkill()
    {
        if (!IsCanUseSkill())
            return;

        if (KeyManager.instance.IsOnceKeyDown("skill_use"))
            UseSkill(_currentSkillIndex + (int)_attack.currentHand * HAND_MAX_COUNT);
    }

    public bool IsCanUseSkill()
    {
        return (!_attack.isAttacking && !_isSkillUsing && !_damagable.isHurt && !_damagable.isDie);
    }

    public void UseSkill(int key)
    {
        if (!(0 <= key && key < activeSkills.Count)) return;
        if (activeSkills[key] == null) return;

        if (chargeUser)
        {
            if (chargeUser.isChargingStart)
                return;
        }

        ActiveUserBase user = FindUser(activeSkills[key].GetType()) as ActiveUserBase;
        if (!user) return;

        if (user.isCanUseSkill)
        {
            if (typeof(ActiveTimeSlowUser).IsInstanceOfType(user))
            {
                ActiveTimeSlowUser time = user as ActiveTimeSlowUser;
                if (!time)
                {
                    TriggerringSkill(activeSkills[key]);
                    return;
                }

                if (!time.isSkillActivated)
                    TriggerringSkill(activeSkills[key]);
            }
            else
            {
                if (typeof(FloorSkillUser).IsInstanceOfType(user))
                {
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z = transform.position.z;
                    useMousePointToWorld = mousePos;
                    ((FloorSkillUser)user).FrameCountReset();
                }
                TriggerringSkill(activeSkills[key]);
            }
        }
        else
            _sfx.PlaySFX("SkillFail");
    }

    public void SkillEnd(System.Type type)
    {
        _isSkillUsing = false;
        if (type == typeof(ActiveTimeSlow) ||
            type == typeof(ActiveYaran)) return;
        
        ActiveUserBase user = FindUser(type) as ActiveUserBase;
        if (!user) return;

        if (type == typeof(MagicArrow))
        {
            if (!((MagicArrowUser)user).isCanCharge)
                user.CoolTimeStart();
        }
        else
            user.CoolTimeStart();
    }

    void TriggerringSkill(SkillBase skill)
    {
        LookAtMouse lookAt = GetComponent<LookAtMouse>();
        lookAt.CalcDir();

        _attack.speedMultiplier = 0.0f;
        _attack.AttackEnd();

        _isSkillUsing = true;

        if (GetSkillHand(typeof(PassiveReversePower)) == GetSkillHand(skill.GetType()))
        {
            skill.isReversePowerUp = isPassiveReversePowerOn;
            isPassiveReversePowerOn = false;
            GameObject.FindObjectOfType<PlayerEffectManager>().EffectOff("PassiveReversePower");
        }

        skill.UseSkill();
    }

    public bool IsPowerUp(System.Type type)
    {
        for (int i = 0; i < activeSkills.Count; ++i)
        {
            if (type.IsInstanceOfType(activeSkills[i]))
                return activeSkills[i].isReversePowerUp;
        }

        return false;
    }

                                // 강화될 스킬이 액티브냐??
    public float PowerUpFigure(bool isActiveSkill)
    {
        if (isActiveSkill)
        {
            for (int i = 0; i < activeSkills.Count; ++i)
            {
                        // 패시브리버스파워 => 액티브스킬 강화
                if (typeof(PassiveReversePower).IsInstanceOfType(activeSkills[i]))
                    return ((PassiveReversePower)activeSkills[i]).figure;
            }
        }
        else
        {
            for (int i = 0; i < activeSkills.Count; ++i)
            {
                        // 액티브리버스파워 => 패시브스킬 강화
                if (typeof(ActiveReversePower).IsInstanceOfType(activeSkills[i]))
                    return ((ActiveReversePower)activeSkills[i]).figure;
            }
        }

        return 0.0f;
    }

    public bool IsInHand(SkillBase skill, HAND hand)
    {
        int start = 5 * (int)hand;
        for (int i = start; i < start + 5 && i < activeSkills.Count; ++i)
        {
            if (skill == activeSkills[i])
                return true;
        }

        return false;
    }

    public HAND GetSkillHand(System.Type type)
    {
        for (int i = 0; i < activeSkills.Count; ++i)
        {
            if (type.IsInstanceOfType(activeSkills[i]))
                return (i < PlayerSkillUsage.HAND_MAX_COUNT ? HAND.LEFT : HAND.RIGHT); 
        }

        return HAND.NONE;
    }
    
    public HAND GetSkillHand<T>() where T : SkillBase
    {
        for (int i = 0; i < activeSkills.Count; ++i)
        {
            if (typeof(T).IsInstanceOfType(activeSkills[i]))
                return (i < 5 ? HAND.LEFT : HAND.RIGHT);
        }

        return HAND.NONE;
    }

    public bool AddSkill(SkillBase skill)
    {
        foreach (var s in activeSkills)
        {
            if (s.GetType() == skill.GetType())
                return false;
        }
        foreach (var s in passiveSkills)
        {
            if (s.GetType() == skill.GetType())
                return false;
        }

        skill.owner = _skillInfo;
        skill.Init();

        //if (typeof(PassiveBase).IsInstanceOfType(skill))
        //{
        //    PassiveBase passive = skill as PassiveBase;

        //    if (currentPassiveCost + passive.cost > totalPassiveCost)
        //        return false;

        //    currentPassiveCost += passive.cost;
        //}

        _skillInfo.AddSkill(skill);

        if (activeSkills.Count == 1)
        {
            if (!_rotUI)
                _rotUI = FindObjectOfType<SkillRotationUIController>();
            if (!_skillSlotPanel)
                _skillSlotPanel = FindObjectOfType<TempActiveSkillPanelController>();

            _currentSkillIndex = 0;
            //SwordChange((int)((ActiveBase)currentSelectSkill).swordType);

            if (_rotUI)
            {
                _rotUI.ChangeIcon(currentSelectSkill, _currentSkillIndex);
                //_rotUI.UpdateUI(SkillRotUIDir.RIGHT);
            }
            if (_skillSlotPanel)
                _skillSlotPanel.UpdateUI(currentSelectSkill);
        }

        if (_skillSlotPanel)
            _skillSlotPanel.ChangeIcons();

        return true;
    }

    public SkillBase FindSkill(System.Type skill)
    {
        return _skillInfo.FindSkill(skill);
    }

    public T FindSkill<T>() where T : SkillBase
    {
        return _skillInfo.FindSkill<T>();
    }

    public List<T> FindSkills<T>(System.Type except = null) where T : SkillBase
    {
        return _skillInfo.FindSkills<T>(except);
    }

    public List<T> FindSkills<T>(HAND hand, System.Type except = null) where T : SkillBase
    {
        return _skillInfo.FindSkills<T>(hand, except);
    }

    public SkillUser FindUser(System.Type type)
    {
        SkillUserManager manager = GetComponentInChildren<SkillUserManager>();
        return manager.FindUser(type);
    }

    public SkillUser FindUser<T>() where T : SkillBase
    {
        SkillUserManager manager = GetComponentInChildren<SkillUserManager>();
        return manager.FindUser(typeof(T));
    }

    public List<T> FindUsers<T>(System.Type except = null) where T : SkillUser
    {
        SkillUserManager manager = GetComponentInChildren<SkillUserManager>();
        return manager.FindUsers<T>(except);
    }

    public List<ICoolTime> FindCoolTimeUsers()
    {
        SkillUserManager manager = GetComponentInChildren<SkillUserManager>();
        return manager.FindCoolTimeUsers();
    }

    //public void SkillUpgrade(int index)
    //{
    //    int realIndex = (int)_attack.currentHand * 5 + index;

    //    if (skills[realIndex] == null) return;

    //    skills[index].Upgrade();
    //}

    public void SkillChange(SkillBase before, SkillBase after)
    {
        _skillInfo.SkillChange(before, after);
    }

    public void ActiveSkillChange(SkillBase newSkill, int targetIndex)
    {
        if (targetIndex >= activeSkills.Count) return;

        SkillBase beforeActive = activeSkills[targetIndex];
        if (_skillInfo.ActiveSkillChange(beforeActive, newSkill, targetIndex))
            _sfx.PlaySFX("equip_active");

        int nullCount = 0;
        for (int i = 0; i < activeSkills.Count; ++i)
        {
            if (activeSkills[i] == null)
                nullCount++;
        }

        if (nullCount == 3)
        {
            _currentSkillIndex = targetIndex;
            //SwordChange((int)((ActiveBase)currentSelectSkill).swordType);

            if (_rotUI)
            {
                _rotUI.ChangeIcon(currentSelectSkill, _currentSkillIndex);
                _rotUI.UpdateUI(SkillRotUIDir.RIGHT);
            }
            if (_skillSlotPanel)
                _skillSlotPanel.UpdateUI(currentSelectSkill);
        }
    }

    public void PassiveSkillChange(SkillBase newPassive)
    {
        PassiveBase before = null;
        if (_skillInfo.passiveSkills.Count != 0)
            before = _skillInfo.passiveSkills[0] as PassiveBase;

        if (before == null &&
            newPassive == null) return;

        _skillInfo.PassiveSkillChange(before, newPassive);
        if (newPassive == null)
        {
            _attack.SwordChange((int)SWORDTYPE.BASE);
            _attack.ChangeHelper(new PlayerNormalAttackHelper());
        }

        _sfx.PlaySFX("equip_passive");
    }

    //public void SwordChange(int swordType)
    //{
    //    if (!_playerEquip) return;

    //    _playerEquip.ChangeSword(swordType);
    //}

    // test
    //private void OnGUI()
    //{
    //    int start = (int)_attack.currentHand * 5;
    //    for (int i = start; i < start + 5 && i < _skills.Count; ++i)
    //    {
    //        GUIStyle temp = new GUIStyle();
    //        temp.alignment = TextAnchor.MiddleCenter;
    //        temp.fontSize = 10;
    //        string text = "";

    //        GUIStyleState style = new GUIStyleState();
    //        if (typeof(ActiveBase).IsInstanceOfType(_skills[i]))
    //        {
    //            temp.fontStyle = ((ActiveBase)_skills[i]).isCanUseSkill ? FontStyle.Bold : FontStyle.Normal;
    //            style.textColor = ((ActiveBase)_skills[i]).isCanUseSkill ? Color.red : Color.gray;

    //            if (typeof(CountableActive).IsInstanceOfType(_skills[i]))
    //                text = string.Format("{0}\n{1} / {2}\n{3} / {4}", _skills[i].GetType().Name, ((CountableActive)_skills[i]).currentCount, ((CountableActive)_skills[i]).maxCount, ((CountableActive)_skills[i]).currentCoolTime, ((CountableActive)_skills[i]).totalCoolTime);
    //            else
    //                text = string.Format("{0}\n{1} / {2}", _skills[i].GetType().Name, ((ActiveBase)_skills[i]).currentCoolTime, ((ActiveBase)_skills[i]).totalCoolTime);
    //        }
    //        else if (typeof(PassiveBase).IsInstanceOfType(_skills[i]))
    //        {
    //            text = string.Format("{0}", _skills[i].GetType().Name);
    //            style.textColor = Color.green;
    //        }

    //        temp.normal = style;

    //        float x = 50 + 100 * (i - (int)_attack.currentHand * 5);
    //        float y = Screen.height - 100;
    //        GUI.Label(new Rect(x, y, 100, 100), text, temp);
    //    }
    //}
}
