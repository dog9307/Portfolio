using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitEffector : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private SpriteRenderer _renderer;
    private Material _effectMat;

    [SerializeField]
    private SpriteRenderer _parentRenderer;
    public SpriteRenderer parentRenderer { get { return _parentRenderer; } set { _parentRenderer = value; } }

    [HideInInspector]
    [SerializeField]
    private EnemyAttackEffector _attackEffector;

    private void Start()
    {
        _effectMat = Instantiate<Material>(Resources.Load<Material>("ShaderEffect/EnemyShader/EnemyWarningShaderUnlit"));
    }

    public void StartHitEffect()
    {
        if (!_renderer || !parentRenderer) return;

        _renderer.material = _effectMat;

        if (_attackEffector)
            _attackEffector.StopAllCoroutines();

        StartCoroutine(HitEffect());
    }

    IEnumerator HitEffect()
    {
        yield return new WaitForEndOfFrame();
        _renderer.sprite = parentRenderer.sprite;
        ApplyAlpha(1.0f);
        yield return new WaitForSeconds(0.1f);
        ApplyAlpha(0.0f);
    }

    void ApplyAlpha(float alpha)
    {
        Color color = _renderer.color;
        color.a = alpha;
        _renderer.color = color;
    }
}
