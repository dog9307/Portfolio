using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiatrisSphereController : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _sphere;
    [SerializeField]
    private ParticleSystem[] _effects;

    [SerializeField]
    FlowerType _colorType;

    float _SizeChangeTime;
    float _currentTime;

    Coroutine _coroutine;

    [SerializeField]
    private LiatrisController _liatris;

    [SerializeField]
    BossAniCotroller _ani;

    // [SerializeField]
    // private LiatrisPlayerTracyingBullet _tracing;

    [SerializeField]
    private SFXPlayer _sfx;

    [HideInInspector]
    public bool _isSphere;
    // Start is called before the first frame update

    void Start()
    {
        _SizeChangeTime = 0.7f;
        _currentTime = 0;
        _isSphere = false;

        if (!_ani) _ani = GetComponent<BossAniCotroller>();
    }

    // Update is called once per frame
    void Update()
    {
        _ani._anim.SetBool("isSphere", _isSphere);
    }
    public void SizeUp()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ShpereSizeUp());
    }

    public void SizeDown()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ShpereSizeDown());
    }
    public void SphereReset()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _sphere.transform.localScale = new Vector3(0, 0, 1);
    }
    public void SphereSetter(float patternID)
    {

        /*
        float targetH = 0.0f;
        _colorType = (FlowerType)Random.Range((int)FlowerType.NONE +1 ,(int)FlowerType.END);       
        switch (_colorType)
        {
            case FlowerType.MoreDamage:
                _colorType = FlowerType.MoreDamage;
                targetH = TowerGardenManager.MoreDamageH;
                break;
            case FlowerType.CoolTimeIncrease:
                _colorType = FlowerType.CoolTimeIncrease;
                targetH = TowerGardenManager.CoolTimeIncreaseH;
                break;
            case FlowerType.Slow:
                _colorType = FlowerType.Slow;
                targetH = TowerGardenManager.SlowH;
                break;
            default:
                break;
        } */

        float targetH = 0.0f;
        // 부채꼴 - 초록색
        if (patternID == 100)
        {
            _colorType = FlowerType.MoreDamage;

            targetH = TowerGardenManager.MoreDamageH;
        }
        // 소환 - 빨간색
        else if (patternID == 101)
        {
            _colorType = FlowerType.AtkDecrease;

            targetH = TowerGardenManager.AtkDecreaseH;
        }
        // 근접 - 주황색
        else if (patternID == 102)
        {
            _colorType = FlowerType.CoolTimeIncrease;

            targetH = 276.0f;
        }
        // 피자 - 노란색
        else if (patternID == 103)
        {
            _colorType = FlowerType.Slow;

            targetH = TowerGardenManager.SlowH;
        }

        _liatris.type = _colorType;

        foreach (var e in _effects)
        {
            var main = e.main;
            var gradient = main.startColor;
            Color color = gradient.color;

            float alpha = color.a;
            float currentH = 0.0f;
            float currentS = 0.0f;
            float currentV = 0.0f;
            Color.RGBToHSV(color, out currentH, out currentS, out currentV);

            currentH = targetH / 360.0f;

            color = Color.HSVToRGB(currentH, currentS, currentV);
            color.a = alpha;
            gradient.color = color;
            main.startColor = gradient;
        }

        //var main = _sphere.main;
    }
    public void TracingSetter()
    {
       // _colorType = (FlowerType)Random.Range((int)FlowerType.NONE + 1, (int)FlowerType.END);
       // if (_colorType == FlowerType.AtkDecrease) TracingSetter();
       //
       // if (_tracing)
       // {
       //     float targetH = 0.0f;
       //
       //     switch (_colorType)
       //     {
       //         case FlowerType.MoreDamage:
       //             _colorType = FlowerType.MoreDamage;
       //             targetH = TowerGardenManager.MoreDamageH;
       //             break;
       //         case FlowerType.CoolTimeIncrease:
       //             _colorType = FlowerType.CoolTimeIncrease;
       //             targetH = TowerGardenManager.CoolTimeIncreaseH;
       //             break;
       //         case FlowerType.Slow:
       //             _colorType = FlowerType.Slow;
       //             targetH = TowerGardenManager.SlowH;
       //             break;
       //         default:
       //             break;
       //     }
       //
       //     _tracing._type = _colorType;
       //     _tracing.ApplyColor(targetH);
       // }
       //
       // 
    }

    [SerializeField]
    private float _targetSize = 1.5f;
    IEnumerator ShpereSizeUp()
    {
        _sfx.PlaySFX("sphereOn");

        Vector3 Scale = _sphere.transform.localScale;
        float scale = 0.0f;
        scale = Mathf.Lerp(0.0f, _targetSize, 0.0f);
        while (_currentTime < _SizeChangeTime)
        {
            float ratio = _currentTime / _SizeChangeTime;

            scale= Mathf.Lerp(0.0f, _targetSize, ratio);

            Scale = new Vector3(scale, scale, scale);
            _sphere.transform.localScale = Scale;

            yield return null;

            _currentTime += IsterTimeManager.bossDeltaTime;
        }
        Scale = new Vector3(_targetSize, _targetSize, _targetSize);
        _sphere.transform.localScale = Scale;
        _isSphere = true;
        //StopCoroutine(_coroutine);
        //yield return null;

        _currentTime = 0;
        _coroutine = null;
    }
    IEnumerator ShpereSizeDown()
    {
        Vector3 Scale = _sphere.transform.localScale;
        float scale = _targetSize;
        scale = Mathf.Lerp(_targetSize, 0.0f, 0.0f);
        while (_currentTime < _SizeChangeTime)
        {
            float ratio = _currentTime / _SizeChangeTime;

            scale = Mathf.Lerp(_targetSize, 0.0f, ratio);

            Scale = new Vector3(scale, scale, scale);
            _sphere.transform.localScale = Scale;

            yield return null;

            _currentTime += IsterTimeManager.bossDeltaTime;
        }
        Scale = new Vector3(0.0f, 0.0f, 0.0f);
        _sphere.transform.localScale = Scale;

        _isSphere = false;
        //_currentTime = 0;
        //StopCoroutine(_coroutine);

        //yield return null;

        _currentTime = 0;
        _coroutine = null;
    }
}