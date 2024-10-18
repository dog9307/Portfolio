using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldLiatrisPartnerController : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private DissolveApplier _dissolve;

    [SerializeField]
    private float _dissolveFadeTime = 0.5f;

    private Coroutine _dissolveCoroutine;
    private Coroutine _moveCoroutine;

    [SerializeField]
    private float _moveTime = 0.1f;
    public float moveTime { get { return _moveTime; } }

    private FieldLiatrisPartnerNodeController _currentNode;

    public FieldLiatrisPartnerManager manager { get; set; }

    private bool _isMove;
    public bool isMove { get { return _isMove; } }

    [HideInInspector]
    [SerializeField]
    private SpriteRenderer _renderer;
    [HideInInspector]
    [SerializeField]
    private ParticleSystem _afterEffect;

    [HideInInspector]
    [SerializeField]
    private Animator _anim;
    public Sprite currentSprite { get { return _renderer.sprite; } }
    [SerializeField]
    private Texture2D _readyTexture;

    void Start()
    {
        _afterEffect.Stop();
    }

    void Update()
    {
        if (_isMove) return;

        bool isFlip = (transform.position.x > manager.transform.position.x);
        if (_anim.enabled)
            _anim.SetBool("isLeft", isFlip);
        else
            _renderer.flipX = isFlip;

        Vector3 scale = _afterEffect.transform.localScale;
        scale.x = (isFlip ? -1.0f : 1.0f);
        _afterEffect.transform.localScale = scale;
    }

    public void ReadyToMove(FieldLiatrisPartnerNodeController firstNode)
    {
        transform.position = firstNode.transform.position;

        _currentNode = firstNode;

        StartFade(1.0f);

        StartCoroutine(MoveReady());
    }

    IEnumerator MoveReady()
    {
        yield return new WaitForSeconds(1.0f);
        _anim.SetTrigger("move");
    }

    public void EndMove()
    {
        StartFade(0.0f);

        if (_moveCoroutine != null)
        {
            _isMove = false;
            StopCoroutine(_moveCoroutine);
        }

        _afterEffect.Stop();
    }

    public void StartFade(float toFade)
    {
        if (_dissolveCoroutine != null)
            StopCoroutine(_dissolveCoroutine);

        _dissolveCoroutine = StartCoroutine(Dissolve(toFade));
    }

    public void Move(FieldLiatrisPartnerNodeController targetNode, Sprite sprite, Texture2D texture)
    {
        if (!_afterEffect.isPlaying)
            _afterEffect.Play();

        _anim.enabled = false;

        if (_moveCoroutine != null)
        {
            _isMove = false;
            StopCoroutine(_moveCoroutine);
        }

        _moveCoroutine = StartCoroutine(MoveToward(targetNode));

        if (sprite)
            _renderer.sprite = sprite;

        if (texture)
        {
            ParticleSystemRenderer particleRenderer = _afterEffect.GetComponent<ParticleSystemRenderer>();
            particleRenderer.material.SetTexture("_BaseMap", texture);
        }
    }

    IEnumerator Dissolve(float toFade)
    {
        _anim.enabled = true;

        float currentTime = 0.0f;
        float startFade = _dissolve.currentFade;
        while (currentTime < _dissolveFadeTime)
        {
            float ratio = currentTime / _dissolveFadeTime;
            float fade = Mathf.Lerp(startFade, toFade, ratio);

            _dissolve.currentFade = fade;

            yield return null;

            currentTime += IsterTimeManager.enemyDeltaTime;
        }
        _dissolve.currentFade = toFade;

        _dissolveCoroutine = null;
    }

    IEnumerator MoveToward(FieldLiatrisPartnerNodeController nextNode)
    {
        _isMove = true;

        float currentTime = 0.0f;
        Vector3 startPos = _currentNode.transform.position;
        _currentNode = nextNode;
        while (currentTime < _moveTime)
        {
            float ratio = currentTime / _moveTime;

            Vector3 newPos = Vector3.Lerp(startPos, nextNode.transform.position, ratio);
            transform.position = newPos;

            yield return null;

            currentTime += IsterTimeManager.enemyDeltaTime;
        }
        transform.position = nextNode.transform.position;

        _moveCoroutine = null;

        _isMove = false;
    }
}
