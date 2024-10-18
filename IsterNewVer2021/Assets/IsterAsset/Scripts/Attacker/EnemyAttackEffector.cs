using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackEffector : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private SpriteRenderer _renderer;
    private Material _effectMat;

    [SerializeField]
    private SpriteRenderer _parentRenderer;
    public SpriteRenderer parentRenderer { get { return _parentRenderer; } set { _parentRenderer = value; } }

    [SerializeField]
    private float _attackEffectTime = 0.3f;

    private void Start()
    {
        _effectMat = Instantiate<Material>(Resources.Load<Material>("ShaderEffect/EnemyShader/EnemyAttackShaderUnlit"));
    }

    public void StartAttackEffect()
    {
        if (!_renderer || !parentRenderer) return;

        _renderer.material = _effectMat;

        StartCoroutine(AttackEffect());
    }

    IEnumerator AttackEffect()
    {
        yield return new WaitForEndOfFrame();

        ApplyAlpha(1.0f);

        float currentTime = 0.0f;
        Vector2 startUV = new Vector2(0.0f, -0.6f);
        Vector2 endUV = new Vector2(0.0f, 0.6f);
        Vector2 currentUV = startUV;
        while (currentTime < _attackEffectTime)
        {
            _renderer.sprite = parentRenderer.sprite;

            float ratio = currentTime / _attackEffectTime;

            currentUV = Vector2.Lerp(startUV, endUV, ratio);
            _effectMat.SetVector("_UvOffset", currentUV);

            yield return null;

            currentTime += IsterTimeManager.enemyDeltaTime;
        }
        currentUV = endUV;
        _effectMat.SetVector("_UvOffset", currentUV);

        yield return null;

        ApplyAlpha(0.0f);
    }

    void ApplyAlpha(float alpha)
    {
        Color color = _renderer.color;
        color.a = alpha;
        _renderer.color = color;
    }
}
