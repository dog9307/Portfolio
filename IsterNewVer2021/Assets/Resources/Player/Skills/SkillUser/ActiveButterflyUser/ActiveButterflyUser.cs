using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveButterflyUser : ActiveUserBase
{
    private PlayerSkillUsage _skillUsage;
    private PlayerAnimController _playerAnimController;
    private PlayerMoveController _playerMove;
    private Rigidbody2D _playerRigid;

    private Vector2 _currentDir;

    private bool _isSkillActivated;
    public bool isSkillActivated { get { return _isSkillActivated; } }

    [SerializeField]
    private float _totalTime = 3.0f;
    private float _currentTime;

    private float _currentSpeed;
    [SerializeField]
    private float _speedMultiplier = 1.7f;

    [SerializeField]
    private GameObject _transformEffect;
    [HideInInspector]
    [SerializeField]
    private ParticleSystem _butterflyEffect;
    [HideInInspector]
    [SerializeField]
    private SpriteRenderer _renderer;

    protected override void Start()
    {
        base.Start();

        _skillUsage = transform.parent.GetComponentInParent<PlayerSkillUsage>();
        _playerAnimController = _skillUsage.GetComponentInParent<PlayerAnimController>();
        _playerMove = _skillUsage.GetComponent<PlayerMoveController>();
        _playerRigid = _skillUsage.GetComponent<Rigidbody2D>();

        _currentDir = Vector2.zero;

        if (_butterflyEffect)
            _butterflyEffect.Stop();
        if (_renderer)
            _renderer.enabled = false;
    }

    void Update()
    {
        if (!_isSkillActivated) return;

        if (KeyManager.instance.IsOnceKeyDown("skill_use"))
            _currentTime = _totalTime;

        TimeCheck();
        Move();
    }

    void Move()
    {
        if (!_isSkillActivated) return;

        _currentDir = Vector2.zero;
        if (KeyManager.instance.IsStayKeyDown("left"))
            _currentDir.x += Vector2.left.x;

        if (KeyManager.instance.IsStayKeyDown("right"))
            _currentDir.x += -Vector2.left.x;

        if (KeyManager.instance.IsStayKeyDown("up"))
            _currentDir.y += Vector2.up.y;

        if (KeyManager.instance.IsStayKeyDown("down"))
            _currentDir.y += -Vector2.up.y;

        _currentDir = _currentDir.normalized;
        Vector2 targetVelocity = _currentDir * _currentSpeed * _speedMultiplier;

        if (Mathf.Abs(_currentDir.x) > 0.0f)
        {
            Vector2 scale = transform.localScale;
            scale.x = -_currentDir.x;
            transform.localScale = scale;
        }

        _playerAnimController.CharacterSetDir(_currentDir, _currentSpeed * _speedMultiplier);

        _playerRigid.velocity = targetVelocity;
        _playerRigid.velocity += _playerMove.additionalForce;
    }

    void TimeCheck()
    {
        _currentTime += IsterTimeManager.deltaTime;
        if (_currentTime >= _totalTime)
            SkillEnd();
    }

    public override void UseSkill()
    {
        if (_isSkillActivated) return;
        if (!_skillUsage) return;

        _playerAnimController.gameObject.layer = LayerMask.NameToLayer("PlayerGhost");

        _isSkillActivated = true;

        _currentTime = 0.0f;

        _currentDir = _playerMove.GetComponent<LookAtMouse>().dir;

        if (!_playerAnimController)
            _playerAnimController = FindObjectOfType<PlayerAnimController>();

        _playerAnimController.ResetTrigger("skillEnd");

        _currentSpeed = _playerMove.speed;

        if (_sfx)
            _sfx.PlaySFX(_sfxName);

        GameObject effect = Instantiate(_transformEffect);
        effect.transform.position = transform.position;

        if (_butterflyEffect)
            _butterflyEffect.Play();
        if (_renderer)
            _renderer.enabled = true;

        _playerMove.dashStartPos = _playerMove.transform.position;
    }


    private PlayerFlyingChecker _fallingChecker;
    public override void SkillEnd()
    {
        if (!_isSkillActivated) return;

        _playerAnimController.gameObject.layer = LayerMask.NameToLayer("Player");

        _isSkillActivated = false;
        _skillUsage.SkillEnd(typeof(ActiveYaran));

        CoolTimeStart();

        if (!_playerAnimController)
            _playerAnimController = FindObjectOfType<PlayerAnimController>();

        _playerAnimController.SkillEnd();

        if (_sfx)
            _sfx.PlaySFX(_sfxName);

        GameObject effect = Instantiate(_transformEffect);
        effect.transform.position = transform.position;

        if (_butterflyEffect)
            _butterflyEffect.Stop();
        if (_renderer)
            _renderer.enabled = false;

        if (!_fallingChecker)
            _fallingChecker = FindObjectOfType<PlayerFlyingChecker>();

        if (_fallingChecker)
            _fallingChecker.Check();
    }
}
