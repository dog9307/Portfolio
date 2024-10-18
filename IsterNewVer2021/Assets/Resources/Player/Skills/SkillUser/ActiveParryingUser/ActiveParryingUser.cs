using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveParryingUser : ActiveUserBase, IObjectCreator
{
    [SerializeField]
    private GameObject _shield;
    private ParryingShieldController _controller;

    [SerializeField]
    private float _totalTime = 0.5f;
    private float _currentTime;
    
    private PlayerAnimController _anim;
    private Damagable _damagable;

    public bool isParryingReady { get; set; }
    public bool isParryingSuccess { get; set; }

    [SerializeField]
    private GameObject _stunBullet;
    public GameObject effectPrefab { get { return _stunBullet; } set { _stunBullet = value; } }

    protected override void Start()
    {
        base.Start();

        Init();
    }

    public override void Init()
    {
        _anim = _info.GetComponent<PlayerAnimController>();
        _damagable = _anim.GetComponent<Damagable>();

        _controller = _shield.GetComponent<ParryingShieldController>();
        _controller.owner = this;

        isParryingReady = false;
    }

    void Update()
    {
        if (!isParryingReady) return;

        _currentTime += IsterTimeManager.deltaTime;
        if (_currentTime >= _totalTime)
            _shield.SetActive(false);
    }

    public override void SkillEnd()
    {
        if (!isParryingReady) return;

        PlayerSkillUsage playerSkill = FindObjectOfType<PlayerSkillUsage>();
        playerSkill.SkillEnd(typeof(ActiveParrying));
        _anim.SkillEnd();
        _anim.Parrying(isParryingSuccess);

        if (isParryingSuccess)
            CreateObject();

        isParryingSuccess = false;
        isParryingReady = false;

        _damagable.isRelativeDirection = false;
        
        if (_shield.activeSelf)
            _shield.SetActive(false);
    }

    public override void UseSkill()
    {
        if (_shield)
            _shield.SetActive(true);

        _controller.EffectSpeedSetting(_totalTime);
        
        _damagable.isRelativeDirection = true;

        isParryingSuccess = false;
        isParryingReady = true;
        _currentTime = 0.0f;
    }

    public GameObject CreateObject()
    {
        GameObject newBullet = Instantiate(effectPrefab);
        newBullet.transform.position = transform.position;

        return newBullet;
    }
}
