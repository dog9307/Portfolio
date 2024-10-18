using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class LiatrisPlayerTracyingBullet : MonoBehaviour
//{
//    [Range(0.0f, 2.0f)] public float _bulletSpeed = 0.5f;

//    public float _inTimeCounter;
//    float _currentCounter;

//    public bool _isInside;
//    public bool _isFire;

//    public FlowerType _type{get;set;}

//    Vector3 _dir;
//    [SerializeField]
//    BossController _bossController;
    
//    public GameObject _damageArea;
//    public GameObject _floor;
//    public GameObject _shpere;

//    [HideInInspector]
//    public Transform _prevPos;

//    Coroutine _coroutine;

//    [SerializeField]
//    private ParticleSystem _bombEffect;

//    [Header("색 바뀔 녀석들")]
//    [SerializeField]
//    private SpriteRenderer _sphere;
//    [SerializeField]
//    private ParticleSystem[] _effects;

//    private void OnEnable()
//    {
//        PlayerMoveController player = FindObjectOfType<PlayerMoveController>();
//        _floor.SetActive(true);
//        _shpere.SetActive(true);
//    }
//    private void OnDisable()
//    {
//        if (_ctCoroutine != null)
//            StopCoroutine(_ctCoroutine);
//    }
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (_bossController.player &&!_isFire)
//        {
//            if (collision.tag.Equals("PLAYER") && _floor.activeSelf)
//                _isInside = true;
//        }
//    }
//    private void OnTriggerExit2D(Collider2D collision)
//    {
//        if (_bossController.player&& !_isFire)
//        {
//            if (collision.tag.Equals("PLAYER"))
//            {
//                _isInside = false;
//                _currentCounter = 0;
//            }
//        }
//    }
//    // Start is called before the first frame update
//    void Start()
//    {
//        _isFire = false;
//    }
//    // Update is called once per frame
//    void Update()
//    {
//        if (_isInside && !_isFire) _currentCounter += IsterTimeManager.bossDeltaTime;

//        if (_currentCounter > _inTimeCounter)
//        {
//            _currentCounter = 0;
//            _isFire = true;
//            GetComponent<Animator>().SetTrigger("isFire");
//        }

//        if (!_isFire)
//        {
//            _prevPos = transform;

//            var targetDir = (_bossController.player.center - transform.position).normalized;
//            Vector3 newPos = transform.position;
//            transform.position = Vector3.Lerp(newPos, _bossController.player.center, _bulletSpeed * IsterTimeManager.bossDeltaTime);

//            if (_bombEffect)
//                _bombEffect.transform.position = transform.position;
//        }
//    }
//    public void FloorDamageOn()
//    {
//        _damageArea.SetActive(true); 
//        _floor.SetActive(false);
//        _shpere.SetActive(false);
//        _isFire = true;

//        if (_bombEffect)
//            _bombEffect.Play();

//        CameraShakeController.instance.CameraShake(15.0f);

//        _ctCoroutine = StartCoroutine(CoolTime(_coolTime));
//    }

//    public void BossGrogi(float coolTime)
//    {
//        _damageArea.SetActive(false);
//        _floor.SetActive(false);
//        _shpere.SetActive(false);
//        _isFire = true;

//        //if (_bombEffect)
//        //    _bombEffect.Play();

//        //CameraShakeController.instance.CameraShake(15.0f);

//        if (_ctCoroutine != null)
//            StopCoroutine(_ctCoroutine);

//        _ctCoroutine = StartCoroutine(CoolTime(coolTime));
//    }


//    [SerializeField]
//    private float _coolTime = 1.0f;
//    private Coroutine _ctCoroutine;
//    IEnumerator CoolTime(float coolTime)
//    {
//        float currentTime = 0.0f;
//        while (currentTime < coolTime)
//        {
//            yield return null;

//            currentTime += IsterTimeManager.bossDeltaTime;
//        }

//        ResetBullet();

//        _ctCoroutine = null;
//    }
//    [SerializeField]
//    private LiatrisPatternBullet _patternBullet;

//    public void ResetBullet()
//    {
//        _floor.SetActive(true);
//        _shpere.SetActive(true);
//        GetComponent<Animator>().SetTrigger("Reset");
//        transform.position = _prevPos.position;
//        _isFire = false;

//        _patternBullet.BulletOff();

//        _currentCounter = 0;
//    }

//    public void BossDie()
//    {
//        if (_ctCoroutine != null)
//            StopCoroutine(_ctCoroutine);

//        _damageArea.SetActive(false);
//        _floor.SetActive(false);
//        _shpere.SetActive(false);
//        _isFire = true;

//        _currentCounter = 0;

//        gameObject.SetActive(false);
//    }

//    // IEnumerator TracyingPlayerFloor()
//    // {
//    //     var targetDir = (player.center - transform.position).normalized;
//    //
//    //         _dir = Vector2.Lerp(_dir, targetDir, IsterTimeManager.bossDeltaTime * _followSpeed);
//    //
//    //         var newPos = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
//    //         Quaternion target = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, newPos + 90);
//    //         transform.rotation = Quaternion.Lerp(transform.rotation, target, IsterTimeManager.bossDeltaTime * _followSpeed);
//    //
//    //         transform.position = _dir * _bulletSpeed;
//    //    
//    //     
//    //     
//    //     _isFire = true;
//    // }

//    public void ApplyColor(float targetH)
//    {
//        Color color = _sphere.color;

//        float alpha = color.a;
//        float currentH = 0.0f;
//        float currentS = 0.0f;
//        float currentV = 0.0f;

//        Color.RGBToHSV(color, out currentH, out currentS, out currentV);
//        currentH = targetH / 360.0f;
//        color = Color.HSVToRGB(currentH, currentS, currentV);

//        color.a = alpha;
//        _sphere.color = color;

//        foreach (var e in _effects)
//            ParticleColorChange(e, targetH);
//    }

//    void ParticleColorChange(ParticleSystem target, float targetH)
//    {
//        Color color = Color.white;

//        var main = target.main;
//        var gradient = main.startColor;
//        color = gradient.color;

//        float alpha = color.a;
//        float currentH = 0.0f;
//        float currentS = 0.0f;
//        float currentV = 0.0f;

//        Color.RGBToHSV(color, out currentH, out currentS, out currentV);
//        currentH = targetH / 360.0f;
//        color = Color.HSVToRGB(currentH, currentS, currentV);

//        color.a = alpha;
//        gradient.color = color;
//        main.startColor = gradient;
//    }
//}
