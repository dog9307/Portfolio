using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ActiveYaranUser : ActiveUserBase, IObjectCreator
{
    private PlayerSkillUsage _skillUsage;
    private PlayerAnimController _playerAnimController;
    private PlayerMoveController _playerMove;
    private Rigidbody2D _playerRigid;

    [SerializeField]
    private float _screenEffectFadeTime = 1.0f;
    [SerializeField]
    private float _brakeTime = 0.5f;

    [SerializeField]
    private GameObject _effectPrefab;
    public GameObject effectPrefab { get { return _effectPrefab; } set { _effectPrefab = value; } }

    [SerializeField]
    private Volume _vol;
    private Coroutine _volumeCoroutine;

    [SerializeField]
    private float _totalTime = 3.0f;
    private float _currentTime;

    private bool _isSkillActivated;
    public bool isSkillActivated { get { return _isSkillActivated; } }
    private bool _isBrakeStart;

    private float _currentSpeed;
    [SerializeField]
    private float _targetSpeedMultiplier = 2.0f;

    private Vector2 _currentDir;

    //private AfterImageController _afterImage;
    [SerializeField]
    private ParticleSystem _moveEffect;

    [SerializeField]
    private GameObject _bullet;

    protected override void Start()
    {
        base.Start();

        _skillUsage = transform.parent.GetComponentInParent<PlayerSkillUsage>();
        _playerAnimController = _skillUsage.GetComponentInParent<PlayerAnimController>();
        _playerMove = _skillUsage.GetComponent<PlayerMoveController>();
        _playerRigid = _skillUsage.GetComponent<Rigidbody2D>();

        _currentDir = Vector2.zero;

        _vol.weight = 0.0f;

        _bullet.SetActive(false);
    }

    void Update()
    {
        if (!_isSkillActivated) return;

        if (KeyManager.instance.IsOnceKeyDown("skill_use"))
            _currentTime = _totalTime;

        TimeCheck();
        Move();
        //TimeScaleUpdate();
    }

    void Move()
    {
        if (!_isSkillActivated) return;

        if (!_isBrakeStart)
            _currentSpeed = _playerMove.speed * _targetSpeedMultiplier;

        if (KeyManager.instance.IsStayKeyDown("left"))
            _currentDir.x = Vector2.left.x;

        if (KeyManager.instance.IsStayKeyDown("right"))
            _currentDir.x = -Vector2.left.x;

        if (KeyManager.instance.IsStayKeyDown("up"))
            _currentDir.y = Vector2.up.y;

        if (KeyManager.instance.IsStayKeyDown("down"))
            _currentDir.y = -Vector2.up.y;

        _currentDir = _currentDir.normalized;
        Vector2 targetVelocity = _currentDir * _currentSpeed;

        _playerAnimController.CharacterSetDir(_currentDir, _currentSpeed);

        _playerRigid.velocity = targetVelocity;
        _playerRigid.velocity += _playerMove.additionalForce;
    }

    void TimeCheck()
    {
        _currentTime += IsterTimeManager.deltaTime;
        if (_currentTime >= _totalTime)
            SkillEnd();
    }

    public GameObject CreateObject()
    {
        if (!effectPrefab)
            return null;

        GameObject newEffect = Instantiate(effectPrefab);
        newEffect.transform.position = transform.position;
        return newEffect;
    }

    public override void UseSkill()
    {
        if (_isSkillActivated) return;
        if (!_skillUsage) return;

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemys"), true);

        _isSkillActivated = true;
        _isBrakeStart = false;

        _currentTime = 0.0f;

        _currentDir = _playerMove.GetComponent<LookAtMouse>().dir;

        CreateObject();

        VolumeWeightChange(1.0f);

        //if (!_afterImage)
        //    _afterImage = FindObjectOfType<AfterImageController>(true);

        //_afterImage.gameObject.SetActive(true);

        if (_moveEffect)
            _moveEffect.Play();

        _bullet.SetActive(true);

        if (!_playerAnimController)
            _playerAnimController = FindObjectOfType<PlayerAnimController>();

        _playerAnimController.ResetTrigger("skillEnd");

        if (_sfx)
            _sfx.PlaySFX("active");

        _chargeAudio = _sfx.PlayLoopSFX("move");
    }

    private AudioSource _chargeAudio;

    public override void SkillEnd()
    {
        if (!_isSkillActivated) return;
        if (_isBrakeStart) return;

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemys"), false);

        _skillUsage.SkillEnd(typeof(ActiveYaran));

        CoolTimeStart();

        if (!_playerAnimController)
            _playerAnimController = FindObjectOfType<PlayerAnimController>();

        _playerAnimController.SkillEnd();

        VolumeWeightChange(0.0f);

        StartCoroutine(Brake());

        if (_moveEffect)
            _moveEffect.Stop();

        _bullet.SetActive(false);

        if (_sfx)
            _sfx.PlaySFX("deactive");

        if (_chargeAudio)
        {
            SoundSystem.instance.StopLoopSFX(_chargeAudio);
            _chargeAudio = null;
        }
    }

    IEnumerator Brake()
    {
        _isBrakeStart = true;

        float currentTime = 0.0f;
        float startSpeed = _currentSpeed;
        while (currentTime < _brakeTime)
        {
            float ratio = currentTime / _brakeTime;

            _currentSpeed = Mathf.Lerp(startSpeed, 0.0f, ratio);

            yield return null;

            currentTime += IsterTimeManager.deltaTime;
        }
        _currentSpeed = 0.0f;

        _isSkillActivated = false;

        //if (!_afterImage)
        //    _afterImage = FindObjectOfType<AfterImageController>(true);

        //_afterImage.gameObject.SetActive(false);
    }

    public void VolumeWeightChange(float toW)
    {
        if (_volumeCoroutine != null)
            StopCoroutine(_volumeCoroutine);

        _volumeCoroutine = StartCoroutine(VolumeWeight(toW));
    }

    IEnumerator VolumeWeight(float toW)
    {
        float startW = _vol.weight;
        float currentTime = 0.0f;
        while (currentTime < _screenEffectFadeTime)
        {
            float ratio = currentTime / _screenEffectFadeTime;

            _vol.weight = Mathf.Lerp(startW, toW, ratio);

            yield return null;

            currentTime += IsterTimeManager.deltaTime;
        }
        _vol.weight = toW;

        _volumeCoroutine = null;
    }
}
