using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JustRunAwaySessackyeeController : MonoBehaviour
{
    [SerializeField]
    private Transform _movePos;
    [SerializeField]
    private float _speedMultiply = 1.0f;

    private bool _isMovestart;

    [SerializeField]
    private SpriteRenderer _renderer;
    private NavMeshAgent _agent;
    private Animator _anim;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.enabled = false;
        _agent.speed *= _speedMultiply;
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _renderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();

        _isMovestart = false;
    }

    void Update()
    {
        Vector2 dir = Vector2.down;
        if (_isMovestart)
        {
            _agent.SetDestination(_movePos.position);

            dir = _agent.desiredVelocity;
        }

        dir = dir.normalized;

        _anim.SetFloat("dirX", dir.x);
        _anim.SetFloat("dirY", dir.y);
    }

    private void OnDisable()
    {
        if (_isMovestart)
        {
            if (_fading != null)
                StopCoroutine(_fading);

            Destroy(gameObject);
        }
    }

    [SerializeField]
    private float _notFadingTime = 1.5f;
    [SerializeField]
    private float _fadingTime = 5.0f;

    private Coroutine _fading;

    public void Surprise()
    {
        _anim.SetTrigger("surprise");
    }

    public void MoveStart()
    {
        _isMovestart = true;

        _agent.enabled = true;

        _fading = StartCoroutine(Fading());
    }

    IEnumerator Fading()
    {
        yield return new WaitForSeconds(_notFadingTime);

        float currentTime = 0.0f;
        while (currentTime < _fadingTime)
        {
            float ratio = currentTime / _fadingTime;

            ApplyAlhap(1.0f - ratio);

            yield return null;

            currentTime += IsterTimeManager.enemyDeltaTime;
        }
        ApplyAlhap(0.0f);
    }

    void ApplyAlhap(float alpha)
    {
        Color color = _renderer.color;
        color.a = alpha;
        _renderer.color = color;
    }

    public void ApplyColor(Color color)
    {
        _renderer.color = color;

        GlowableObject glow = GetComponent<GlowableObject>();
        if (glow)
            glow.ApplyColor(color);
    }
}
