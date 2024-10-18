using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItemRerollUser : CountableUserBase
{
    private Damagable _player;
    private Animator _anim;

    [SerializeField]
    private float _chargingTime = 1.0f;
    private float _currentTime;

    [SerializeField]
    private ParticleSystem _chargingEffect;
    [SerializeField]
    private ParticleSystem _healEffect;

    private bool _isSkillUsing;

    public override bool isCanUseSkill { get { return !IsCoolTime(); } }

    protected override void Start()
    {
        base.Start();

        _player = FindObjectOfType<PlayerMoveController>().GetComponent<Damagable>();
        _anim = _player.GetComponent<Animator>();

        _isSkillUsing = false;
    }

    void Update()
    {
        if (!_isSkillUsing) return;

        if (IsPressCancleMotion() || !KeyManager.instance.IsStayKeyDown("skill_use"))
        {
            SkillEnd();
            return;
        }

        _currentTime += IsterTimeManager.deltaTime;
        if (_currentTime >= _chargingTime)
            PlayerHeal();
    }

    void PlayerHeal()
    {
        _currentCount--;

        _healEffect.Play();

        _sfx.PlaySFX("heal");

        _player.Heal(20.0f);

        SkillEnd();
    }

    bool IsPressCancleMotion()
    {
        bool isCancle =
            KeyManager.instance.IsOnceKeyDown("left") ||
            KeyManager.instance.IsOnceKeyDown("right") ||
            KeyManager.instance.IsOnceKeyDown("up") ||
            KeyManager.instance.IsOnceKeyDown("down") ||
            KeyManager.instance.IsOnceKeyDown("tabUI") ||
            KeyManager.instance.IsOnceKeyDown("dash");

        return isCancle;
    }

    public override void UseSkill()
    {
        _isSkillUsing = true;

        _anim.SetBool("isHealCharging", true);

        _currentTime = 0.0f;

        if (_chargingEffect.isPlaying)
            _chargingEffect.Stop();

        _chargingEffect.Play();

        _sfx.PlaySFX(_sfxName);
    }

    public void ChargeAll()
    {
        currentCount = maxCount;
    }

    public override void SkillEnd()
    {
        _isSkillUsing = false;

        _anim.SetBool("isHealCharging", false);

        if (_chargingEffect.isPlaying)
            _chargingEffect.Stop();
    }

    public override void CoolTimeStart()
    {
    }

    public override IEnumerator CoolTimeUpdate()
    {
        yield return null;
    }

    public override void CoolTimeDown(float time)
    {
    }

    public override bool IsCoolTime()
    {
        return (_player.currentHP >= _player.totalHP) || (_currentCount <= 0);
    }
}
