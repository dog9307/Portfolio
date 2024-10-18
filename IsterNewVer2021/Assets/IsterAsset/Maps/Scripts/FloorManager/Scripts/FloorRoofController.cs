using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloorRoofController : MonoBehaviour
{
    [SerializeField]
    private List<SpriteRenderer> _sprites;
    [SerializeField]
    private List<Tilemap> _tiles;

    private Dictionary<SpriteRenderer, float> _spriteStartAlphaDic = new Dictionary<SpriteRenderer, float>();
    private Dictionary<Tilemap, float> _tileStartAlphaDic = new Dictionary<Tilemap, float>();

    [SerializeField]
    private float _totalTime = 0.5f;
    private float _currentTime = 0.0f;

    private float _currentAlpha;

    private Coroutine _fadeCoroutine;

    void Start()
    {
        _currentAlpha = 1.0f;

        foreach (var s in _sprites)
            _spriteStartAlphaDic.Add(s, s.color.a);
        foreach (var t in _tiles)
            _tileStartAlphaDic.Add(t, t.color.a);
    }

    public void AddSprite(SpriteRenderer sprite)
    {
        _sprites.Add(sprite);
        _spriteStartAlphaDic.Add(sprite, sprite.color.a);
    }

    public void AddTileMap(Tilemap tilemap)
    {
        _tiles.Add(tilemap);
        _tileStartAlphaDic.Add(tilemap, tilemap.color.a);
    }

    public void Appear()
    {
        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);

        _fadeCoroutine = StartCoroutine(Fade(1.0f));
    }

    public void Disappear()
    {
        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);

        _fadeCoroutine = StartCoroutine(Fade(0.0f));
    }

    IEnumerator Fade(float toAlpha)
    {
        float fromAlpha = (toAlpha == 1.0f ? 0.0f : 1.0f);

        _currentTime = (_currentAlpha - fromAlpha) / (toAlpha - fromAlpha);
        while (_currentTime < _totalTime)
        {
            float ratio = _currentTime / _totalTime;

            _currentAlpha = Mathf.Lerp(fromAlpha, toAlpha, ratio);
            ApplyAlpha(_currentAlpha);

            yield return null;

            _currentTime += IsterTimeManager.originDeltaTime;
        }
        _currentAlpha = toAlpha;
        ApplyAlpha(_currentAlpha);
    }

    void ApplyAlpha(float alpha)
    {
        foreach (var s in _sprites)
        {
            if (!s) continue;
            if (!_spriteStartAlphaDic.ContainsKey(s)) continue;

            Color color = s.color;
            color.a = alpha * _spriteStartAlphaDic[s];
            s.color = color;
        }
        foreach (var t in _tiles)
        {
            if (!t) continue;
            if (!_tileStartAlphaDic.ContainsKey(t)) continue;

            Color color = t.color;
            color.a = alpha * _tileStartAlphaDic[t];
            t.color = color;
        }
    }
}
