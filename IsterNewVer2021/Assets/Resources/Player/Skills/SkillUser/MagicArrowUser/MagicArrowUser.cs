using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowUser : CountableUserBase, IObjectCreator
{
    public GameObject effectPrefab { get; set; }

    private NormalBulletCreator _playerBulletCreator;
    public NormalBulletCreator bulletCreator { get { return _playerBulletCreator; }  set { _playerBulletCreator = value; } }

    private MagicArrowUserHelperBase _currentHelper;
    public T GetHelper<T>() where T : MagicArrowUserHelperBase
    {
        return (T)_currentHelper;
    }

    public bool isCanCharge { get; set; }
    public override bool isCanUseSkill { get { return (currentCount > 0) || isCanCharge; } }

    [SerializeField]
    private float _defaultDamage = 7.0f;
    public float defaultDamage { get { return _defaultDamage; } }
    // Skill Tree System
    public float additionalDamage { get; set; }

    protected override void Start()
    {
        base.Start();

        bulletCreator = FindObjectOfType<PlayerMoveController>().GetComponentInChildren<NormalBulletCreator>();

        ApplyHelper(new NormalMagicArrowHelper());
    }

    void Update()
    {
        if (_currentHelper != null)
            _currentHelper.Update();
    }
    
    public void ApplyHelper(MagicArrowUserHelperBase helper)
    {
        if (_currentHelper != null)
            _currentHelper.Release();

        _currentHelper = helper;
        _currentHelper.owner = this;
        _currentHelper.Init();
    }

    public GameObject CreateObject()
    {
        GameObject newBullet = null;
        if (_currentHelper != null)
            newBullet = _currentHelper.CreateObject();

        return newBullet;
    }

    public override void UseSkill()
    {
        GameObject newBullet = CreateObject();
        if (_currentHelper != null && newBullet)
            _currentHelper.UseSkill(newBullet);
    }
}
