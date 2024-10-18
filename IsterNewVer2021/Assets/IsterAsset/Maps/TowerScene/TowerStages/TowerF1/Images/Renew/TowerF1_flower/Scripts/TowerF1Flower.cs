using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlowerType
{
    NONE = -1,
    MoreDamage,
    AtkDecrease,
    CoolTimeIncrease,
    Slow,
    END
}

public class TowerF1Flower : MonoBehaviour
{
    [SerializeField]
    private FlowerType _type;
    public FlowerType type { get { return _type; } }

    [SerializeField]
    private BossAniCotroller _liatris;

    [SerializeField]
    private TowerF1FlowerAnimController _anim;

    public bool isDieFlower { get; set; }

    [SerializeField]
    private GlowUpAndDown _glow;
    private float _startMin;
    private float _startMax;
    [SerializeField]
    private SpriteRenderer _renderer;
    private Color _startColor;
    [SerializeField]
    private ParticleSystem _effect;

    [SerializeField]
    private SFXPlayer _sfx;

    void Start()
    {
        _startMin = _glow.minIntensity;
        _startMax = _glow.maxIntensity;

        _startColor = _renderer.color;

        isDieFlower = false;
    }

    public void BossGrogiSignal(FlowerType type)
    {
        if (_type == type)
        {
            if (_liatris)
                _liatris.Grogi();

            if (_anim)
                _anim.Hit();

            _sfx.PlaySFX("dropFlower");
        }
    }

    public void RevivalFlowers()
    {
        if (type == FlowerType.AtkDecrease)
            return;

        isDieFlower = true;

        if (_effect)
            _effect.Play();

        StartCoroutine(ResetFlower());
    }

    IEnumerator ResetFlower()
    {
        _sfx.PlaySFX("flowerRevival");

        float currentTime = 0.0f;
        while (currentTime < 0.5f)
        {
            float ratio = currentTime / 0.5f;

            _glow.minIntensity = Mathf.Lerp(0.0f, _startMin, ratio);
            _glow.maxIntensity = Mathf.Lerp(0.0f, _startMax, ratio);

            _renderer.color = Color.Lerp(Color.white, _startColor, ratio);

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }
        _glow.minIntensity = _startMin;
        _glow.maxIntensity = _startMax;
        _renderer.color = _startColor;

        _renderer.GetComponent<Animator>().enabled = true;
    }
}
