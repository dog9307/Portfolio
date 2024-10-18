using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _sprite;

    private float _fadingTime = 1.0f;
    private float _moveSpeed;
    private float _moveTime;

    [SerializeField]
    [Range(0.0f, 1.0f)] private float _fadingMultuplier = 1.0f;

    [SerializeField]
    private float _moveMinTime = 10.0f;
    [SerializeField]
    private float _moveMaxTime = 20.0f;

    [SerializeField]
    private float _moveMinSpeed = 0.5f;
    [SerializeField]
    private float _moveMaxSpeed = 3.0f;

    void OnEnable()
    {
        if (!_sprite)
            _sprite = GetComponent<SpriteRenderer>();

        StartCoroutine(MoveStart());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator MoveStart()
    {
        while (!gameObject.activeSelf)
            yield return null;

        StartCoroutine(Fading(0.0f, 1.0f));

        Vector2 screenPosMin = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, 0.0f));
        Vector2 screenPosMax = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));

        float startX = Random.Range(screenPosMin.x, screenPosMin.x + ((screenPosMax.x - screenPosMin.x) / 4.0f)); ;
        float startY = Random.Range(screenPosMin.y, screenPosMax.y);

        transform.position = new Vector3(startX, startY, 0.0f);

        _moveTime = Random.Range(_moveMinTime, _moveMaxTime);
        _moveSpeed = Random.Range(_moveMinSpeed, _moveMaxSpeed);

        StartCoroutine(Moving());
        StartCoroutine(MoveEndDelay());
    }

    IEnumerator Moving()
    {
        float currentTime = 0.0f;
        while (currentTime < _moveTime)
        {
            Move(_moveSpeed * IsterTimeManager.enemyDeltaTime);

            yield return null;

            currentTime += IsterTimeManager.enemyDeltaTime;
        }
    }

    IEnumerator MoveEndDelay()
    {
        float currentTime = 0.0f;
        while (currentTime < _moveTime - _fadingTime)
        {
            yield return null;

            currentTime += IsterTimeManager.enemyDeltaTime;
        }

        MoveEnd();
    }

    void Move(float deltaX)
    {
        Vector3 pos = transform.position;
        pos.x += deltaX;
        transform.position = pos;
    }

    void MoveEnd()
    {
        StartCoroutine(Fading(1.0f, 0.0f, true));
    }

    IEnumerator Fading(float fromAlpha, float toAlpha, bool isEnd = false)
    {
        float currentTime = 0.0f;
        while (currentTime < _fadingTime)
        {
            float ratio = currentTime / _fadingTime;
            float alpha = Mathf.Lerp(fromAlpha, toAlpha, ratio);

            ApplyAlpha(alpha);

            yield return null;

            currentTime += IsterTimeManager.enemyDeltaTime;
        }
        ApplyAlpha(toAlpha);

        if (isEnd)
        {
            StopAllCoroutines();
            StartCoroutine(MoveStart());
        }
    }

    void ApplyAlpha(float alpha)
    {
        Color color = _sprite.color;
        color.a = alpha * _fadingMultuplier;
        _sprite.color = color;
    }
}
