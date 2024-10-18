using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveDashUser : CountableUserBase, IObjectCreator
{
    private PlayerAttacker _attack;
    private PlayerMoveController _move;
    private PlayerAnimController _anim;

    public static float STANDARD_DELAY = 0.3f;
    public float startNormalizedTime { get; set; }

    public float coolTimeMultipler { get; set; } = 1.0f;
    public bool isSpeedUp { get; set; }

    [SerializeField]
    private GameObject _prefab;
    public GameObject effectPrefab { get { return _prefab; } set { _prefab = value; } }

    public bool isDumyCreate { get; set; }
    public float additionalDumyTime { get; set; }
    public float additionalDumyHP { get; set; }
    public float scaleFactor { get; set; }

    public override float totalCoolTime
    {
        get { return base.totalCoolTime * coolTimeMultipler; }
        set => base.totalCoolTime = value;
    }

    [SerializeField]
    private GameObject _effectPrefab;

    [SerializeField]
    private float _animationFrameRate = 60.0f;
    [SerializeField]
    private float _animationTotalFrame = 18.0f;
    [SerializeField]
    private float _animationHorizontalTotalFrame = 21.0f;
    private float _currentTime = 0.0f;

    [SerializeField]
    private float _speedMultiplier = 2.9f;

    private float totalDashTime
    {
        get
        {
            Vector2 dir = _attack.dir;
            dir.x = Mathf.Abs(dir.x);
            float degree = CommonFuncs.DegreeBtweenTwoVector(Vector2.right, dir);

            float time = (degree < 45.0f ? _animationHorizontalTotalFrame : _animationTotalFrame) / _animationFrameRate;

            return time;
        }
    }
    private bool _isDashing;

    protected override void Start()
    {
        base.Start();

        _attack = transform.parent.GetComponentInParent<PlayerAttacker>();
        _move = _attack.GetComponent<PlayerMoveController>();
        _anim = _attack.GetComponent<PlayerAnimController>();

        coolTimeMultipler = 1.0f;
        scaleFactor = 1.0f;

        _isDashing = false;
    }

    void FixedUpdate()
    {
        if (!_isDashing) return;

        _currentTime += IsterTimeManager.fixedDeltaTime;
        if (_currentTime < totalDashTime * STANDARD_DELAY)
            _attack.speedMultiplier = 0.0f;
        else if (_currentTime < totalDashTime * 0.9f)
            _attack.speedMultiplier = _speedMultiplier;
        else
            _attack.speedMultiplier = 0.0f;
    }

    public override void UseSkill()
    {
        Vector2 dir = Vector2.zero;

        if (KeyManager.instance.IsStayKeyDown("left", true))
            dir += Vector2.left;

        if (KeyManager.instance.IsStayKeyDown("right", true))
            dir += -Vector2.left;

        if (KeyManager.instance.IsStayKeyDown("up", true))
            dir += Vector2.up;

        if (KeyManager.instance.IsStayKeyDown("down", true))
            dir += -Vector2.up;

        dir = dir.normalized;

        if (dir.magnitude == 0.0f)
            dir = GameObject.FindObjectOfType<LookAtMouse>().dir;
        else
            GameObject.FindObjectOfType<LookAtMouse>().dir = dir;

        _move.keyDir = dir;

        _anim.CharacterSetDir(dir, 0.0f);

        _attack.dir = dir;
        _attack.speedMultiplier = 0.0f;

        if (isDumyCreate)
            CreateObject();

        if (_sfx)
            _sfx.PlaySFX(_sfxName);

        Invoke("CreateEffect", _effectInvokeTime);
        //CreateEffect();

        _move.dashStartPos = _move.transform.position;

        _isDashing = true;
        _currentTime = startNormalizedTime * totalDashTime;
    }

    public override void SkillEnd()
    {
        base.SkillEnd();

        _isDashing = false;
    }

    [SerializeField]
    private float _effectInvokeTime = 0.1f;
    public void CreateEffect()
    {
        if (_effectPrefab)
        {
            GameObject dashEffect = Instantiate(_effectPrefab);
            dashEffect.transform.position = _attack.GetComponent<PlayerMoveController>().center;

            float angle = CommonFuncs.DirToDegree(_attack.dir);
            dashEffect.transform.rotation = Quaternion.identity;
            dashEffect.transform.Rotate(new Vector3(0.0f, 0.0f, angle));
        }
    }

    public void SpeedUp()
    {
        if (!isSpeedUp) return;

        PlayerMoveController player = GameObject.FindObjectOfType<PlayerMoveController>();
        PlayerEffectManager em = player.GetComponentInChildren<PlayerEffectManager>();
        GameObject effect = em.FindEffect("PassiveSpeedUp");
        if (effect)
        {
            SpeedUpEffect speedUp = effect.GetComponent<SpeedUpEffect>();
            if (!speedUp) return;

            speedUp.totalTime = 1.0f;
            speedUp.figure = 2.0f;

            if (effect.activeSelf)
                speedUp.BuffOn();
            else
                effect.SetActive(true);
        }
    }

    public GameObject CreateObject()
    {
        GameObject newDumy = Instantiate(effectPrefab);
        newDumy.transform.position = transform.position;

        DashDumyLookAtMe dumyLookAtMe = newDumy.GetComponentInChildren<DashDumyLookAtMe>();
        dumyLookAtMe.user = this;
        dumyLookAtMe.gameObject.SetActive(false);

        return newDumy;
    }
}
