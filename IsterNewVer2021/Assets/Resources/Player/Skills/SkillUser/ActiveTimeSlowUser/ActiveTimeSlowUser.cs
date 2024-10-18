using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ActiveTimeSlowUser : ActiveUserBase
{
    private PlayerSkillUsage _skillUsage;
    private ActiveTimeSlow _skill;

    [SerializeField]
    [Range(0.0f, 1.0f)] private float _enemyTimeScaleMin;
    public float additionalEnemyTimeScale { get; set; }
    public float enemyTimeScaleMin { get { float figure = (_enemyTimeScaleMin - additionalEnemyTimeScale); return (figure < 0.0f ? 0.0f : figure); } }
    [SerializeField]
    [Range(0.0f, 1.0f)] private float _bossTimeScaleMin;
    public float additionalBossTimeScale { get; set; }
    public float bossTimeScaleMin { get { float figure = (_bossTimeScaleMin - additionalBossTimeScale); return (figure < 0.0f ? 0.0f : figure); } }

    public string skillKey { get; set; }

    [SerializeField]
    private float _totalTime;
    private float _currentTime;

    public float additionalTime { get; set; }
    public float additionalTimeByKill { get; set; }
    public float totalTime { get { return (_totalTime + additionalTime + additionalTimeByKill); } }

    [SerializeField]
    [Range(0.0f, 1.0f)] private float _lerpDelayTime;
    private float _lerpCurrentTime;

    private bool _isSkillActivated;
    public bool isSkillActivated { get { return _isSkillActivated; } }

    public bool isSpeedUp { get; set; }
    public float speedUpFigure { get; set; }

    public bool isTimeUpByKill { get; set; }

    public bool isStayKey { get; set; }

    [SerializeField]
    private Transform _grayScale;

    [SerializeField]
    private Volume _vol;

    [SerializeField]
    private ParticleSystem _activeEffect;

    public class TimeSlowUserHandChanger : SkillUserHandChanger<ActiveTimeSlow>
    {
        public ActiveTimeSlowUser owner { get; set; }

        public override void BuffOn()
        {
        }

        public override void BuffOff()
        {
            owner.SkillEnd();
        }
    }
    private TimeSlowUserHandChanger _helper;

    protected override void Start()
    {
        base.Start();

        _skillUsage = transform.parent.GetComponentInParent<PlayerSkillUsage>();

        PlayerAttacker attack = transform.parent.GetComponentInParent<PlayerAttacker>();
        DualSword sonComponent = attack.GetComponent<DualSword>();
        
        _skill = _skillUsage.FindSkill<ActiveTimeSlow>();

        if (sonComponent)
        {
            HAND relativeHand = _skillUsage.GetSkillHand(_skill.GetType());

            _helper = new TimeSlowUserHandChanger();
            _helper.owner = this;
            _helper.Init(_buff, attack, _skill, relativeHand);
        }

        _lerpCurrentTime = _lerpDelayTime;

        GrayScaleRange(0.0f);

        isStayKey = false;
        _vol.weight = 0.0f;
    }
    
    void Update()
    {
        if (_helper != null)
            _helper.Update();

        if (isStayKey)
            StayKeyCheck();
        else
            TimeCheck();
        TimeScaleUpdate();
    }

    public void TimeUpByKill()
    {
        if (!isTimeUpByKill) return;

        additionalTimeByKill += 10.0f;
    }

    void StayKeyCheck()
    {
        if (_isSkillActivated)
        {
            // before skill rotation
            //if (KeyManager.instance.IsStayKeyDown(skillKey))
            // after skill rotation
            if (KeyManager.instance.IsStayKeyDown("skill_use"))
            {
                _currentTime += IsterTimeManager.deltaTime;
                if (_currentTime >= totalTime)
                    SkillEnd();
            }
            else
                SkillEnd();
        }
    }

    void TimeCheck()
    {
        _currentTime += IsterTimeManager.deltaTime;
        if (_currentTime >= totalTime)
            SkillEnd();
    }

    void TimeScaleUpdate()
    {
        float enemyTimeScaleStart = 1.0f;
        float bossTimeScaleStart = 1.0f;

        float enemyTimeScaleEnd = 1.0f;
        float bossTimeScaleEnd = 1.0f;

        if (_isSkillActivated)
        {
            enemyTimeScaleStart = 1.0f;
            bossTimeScaleStart = 1.0f;
            enemyTimeScaleEnd = enemyTimeScaleMin;
            bossTimeScaleEnd = bossTimeScaleMin;
        }
        else
        {
            enemyTimeScaleStart = enemyTimeScaleMin;
            bossTimeScaleStart = bossTimeScaleMin;
            enemyTimeScaleEnd = 1.0f;
            bossTimeScaleEnd = 1.0f;
        }

        _lerpCurrentTime += IsterTimeManager.deltaTime;
        if (_lerpCurrentTime > _lerpDelayTime)
            _lerpCurrentTime = _lerpDelayTime;

        float ratio = _lerpCurrentTime / _lerpDelayTime;
        IsterTimeManager.enemyTimeScale = Mathf.Lerp(enemyTimeScaleStart, enemyTimeScaleEnd, ratio);
        IsterTimeManager.bossTimeScale = Mathf.Lerp(bossTimeScaleStart, bossTimeScaleEnd, ratio);
    }

    private AudioSource _loop;
    private Coroutine _delay;
    public override void UseSkill()
    {
        if (_isSkillActivated) return;
        if (!_skillUsage) return;
        
        // before skill rotation
        //int index = -1;
        //for (int i = 0; i < _skillUsage.activeSkills.Count; ++i)
        //{
        //    if (typeof(ActiveTimeSlow).IsInstanceOfType(_skillUsage.activeSkills[i]))
        //    {
        //        index = i;
        //        break;
        //    }
        //}
        //if (index == -1) return;

        //skillKey = "skill" + ((index % PlayerSkillUsage.HAND_MAX_COUNT) + 1);

        _skillUsage.SkillEnd(typeof(ActiveTimeSlow));

        _isSkillActivated = true;

        _currentTime = 0.0f;
        _lerpCurrentTime = 0.0f;

        GrayScaleRange(30.0f);

        if (isSpeedUp)
            SpeedUp();

        additionalTimeByKill = 0.0f;

        StartGrayScale();

        if (_sfx)
            _sfx.PlaySFX("active");

        _delay = StartCoroutine(SoundDelay());

        if (_activeEffect)
            _activeEffect.Play();
    }

    public override void SkillEnd()
    {
        if (!_isSkillActivated) return;
        
        _isSkillActivated = false;

        _lerpCurrentTime = 0.0f;

        CoolTimeStart();

        //GrayScaleRange(0.0f);

        if (isSpeedUp)
            SpeedUpEnd();

        EndGrayScale();

        if (_sfx)
            _sfx.PlaySFX("deactive");

        if (_loop)
        {
            SoundSystem.instance.StopLoopSFX(_loop);
            _loop = null;
        }

        if (_delay != null)
            StopCoroutine(_delay);
    }

    IEnumerator SoundDelay()
    {
        yield return new WaitForSeconds(0.3f);

        if (_loop)
        {
            SoundSystem.instance.StopLoopSFX(_loop);
            _loop = null;
        }

        if (_isSkillActivated)
            _loop = _sfx.PlayLoopSFX("timeslowing");

        _delay = null;
    }

    void GrayScaleRange(float scale)
    {
        if (!_grayScale) return;

        _grayScale.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void SpeedUp()
    {
        PlayerMoveController player = GameObject.FindObjectOfType<PlayerMoveController>();
        PlayerEffectManager em = player.GetComponentInChildren<PlayerEffectManager>();
        GameObject effect = em.FindEffect("PassiveSpeedUp");
        if (effect)
        {
            SpeedUpEffect speedUp = effect.GetComponent<SpeedUpEffect>();
            if (!speedUp) return;

            speedUp.totalTime = totalTime;
            speedUp.figure = speedUpFigure;

            if (effect.activeSelf)
                speedUp.BuffOn();
            else
                effect.SetActive(true);
        }
    }

    public void SpeedUpEnd()
    {
        PlayerMoveController player = GameObject.FindObjectOfType<PlayerMoveController>();
        PlayerEffectManager em = player.GetComponentInChildren<PlayerEffectManager>();
        GameObject effect = em.FindEffect("PassiveSpeedUp");
        if (effect)
        {
            SpeedUpEffect speedUp = effect.GetComponent<SpeedUpEffect>();
            if (!speedUp) return;

            effect.SetActive(false);
        }
    }

    private int _coroutineCount = 0;
    private float _currentGrayingTime = 0.0f;
    private float _currentRatio = 0.0f;
    void StartGrayScale()
    {
        StartCoroutine(Graying(1.0f));
    }
    void EndGrayScale()
    {
        StartCoroutine(Graying(0.0f));
    }

    IEnumerator Graying(float toRatio)
    {
        _coroutineCount++;
        _currentGrayingTime = _lerpDelayTime - _currentGrayingTime;
        float startRatio = _currentRatio;
        float totalTime = 1.5f;
        while (_currentGrayingTime < totalTime)
        {
            float ratio = _currentGrayingTime / totalTime;

            _currentRatio = Mathf.Lerp(startRatio, toRatio, ratio);
            _vol.weight = _currentRatio;

            yield return null;

            _currentGrayingTime += IsterTimeManager.deltaTime;

            if (_coroutineCount > 1)
                break;
        }
        _currentGrayingTime = _lerpDelayTime;

        _currentRatio = Mathf.Lerp(startRatio, toRatio, 1.0f);
        _vol.weight = _currentRatio;

        _coroutineCount--;
    }
}
