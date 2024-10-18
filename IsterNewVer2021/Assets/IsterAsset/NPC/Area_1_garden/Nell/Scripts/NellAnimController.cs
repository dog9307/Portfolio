using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Yarn.Unity;

public class NellAnimController : AnimController
{
    [SerializeField]
    private SpriteRenderer _sprite;

    [Header("³ªºñ Æû")]
    [SerializeField]
    private bool _isButterflyForm = true;
    public bool isButterflyForm { get { return _isButterflyForm; } set { _isButterflyForm = value; } }
    [SerializeField]
    private float _frequency = 0.7f;
    [SerializeField]
    private float _moveRange = 0.1f;
    private float _moveCurrentTime = 0.0f;

    private Vector2 _toPlayerDir;
    private PlayerMoveController _player;

    [SerializeField]
    private NavMeshAgent _agent;

    [SerializeField]
    private ParticleSystem _effect;

    void Start()
    {
        _moveCurrentTime = 0.0f;
    }

    void Update()
    {
        _sprite.transform.rotation = Quaternion.identity;
        if (_isButterflyForm)
        {
            _moveCurrentTime += IsterTimeManager.enemyDeltaTime * _frequency;
            if (_moveCurrentTime > 1.0f)
            {
                float x = Random.Range(-_moveRange, _moveRange);
                float y = Random.Range(-_moveRange, _moveRange);

                _sprite.transform.localPosition = new Vector2(x, y);

                _moveCurrentTime = 0.0f;
            }
        }

        if (_agent.desiredVelocity.magnitude <= 0.0f)
        {
            if (!_player)
                _player = FindObjectOfType<PlayerMoveController>();

            _toPlayerDir = CommonFuncs.CalcDir(this, _player);

            _sprite.flipX = (_toPlayerDir.x > 0.0f);
        }
        else
        {
            _sprite.flipX = (_agent.desiredVelocity.x > 0.0f);
        }

        _anim.SetBool("isButterfly", isButterflyForm);
        _anim.SetFloat("velocity", _agent.velocity.magnitude);
    }

    public void Transformation()
    {
        isButterflyForm = !isButterflyForm;

        if (_effect)
            _effect.Play();
    }

    [YarnCommand("NellFormCheck")]
    public void NellFormCheck()
    {
        InMemoryVariableStorage storage = FindObjectOfType<InMemoryVariableStorage>();
        if (!storage) return;

        storage.SetValue("$isNellButterfly", _isButterflyForm);
    }
}
