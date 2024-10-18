using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldFlowerSphere : MonoBehaviour
{
    [Range(0.0f, 2.0f)] public float _bulletSpeed = 0.5f;

    public float _inTimeCounter;
    float _currentCounter;

    public bool _isInside;
    public bool _isFire;

    Vector3 _dir;
    [SerializeField]
    FieldLiatrisController _bossController;
    public GameObject _damageArea;
    public GameObject _floor;
    public GameObject _shpere;

    [HideInInspector]
    public Transform _prevPos;

    [SerializeField]
    private ParticleSystem _bombEffect;

    [SerializeField]
    public float _coolTime;
    public Coroutine _coroutine;

    private void OnEnable()
    {
        PlayerMoveController player = FindObjectOfType<PlayerMoveController>();
        _floor.SetActive(true);
        _shpere.SetActive(true);
        _isFire = false;
    }
    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_bossController.player && !_isFire)
        {
            if (collision.tag.Equals("PLAYER") && _floor.activeSelf)
                _isInside = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_bossController.player && !_isFire)
        {
            if (collision.tag.Equals("PLAYER"))
            {
                _isInside = false;
                _currentCounter = 0;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _isFire = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (_isInside && !_isFire) _currentCounter += IsterTimeManager.bossDeltaTime;

        if (_currentCounter > _inTimeCounter)
        {
            _currentCounter = 0;
            _isFire = true;
            GetComponent<Animator>().SetTrigger("isFire");
        }

        if (!_isFire)
        {
            _prevPos = transform;

            var targetDir = (_bossController.player.center - transform.position).normalized;
            Vector3 newPos = transform.position;
            transform.position = Vector3.Lerp(newPos, _bossController.player.center, _bulletSpeed * IsterTimeManager.bossDeltaTime);

            if (_bombEffect)
                _bombEffect.transform.position = transform.position;
        }
    }
    public void FloorDamageOn()
    {
        _damageArea.SetActive(true);
        _floor.SetActive(false);
        _shpere.SetActive(false);
        _isFire = true;

        if (_bombEffect)
            _bombEffect.Play();

        CameraShakeController.instance.CameraShake(15.0f);

        _coroutine = StartCoroutine(CoolTime(_coolTime));
    }

    IEnumerator CoolTime(float coolTime)
    {
        float currentTime = 0.0f;
        while (currentTime < coolTime)
        {
            yield return null;

            currentTime += IsterTimeManager.bossDeltaTime;
        }

        ResetBullet();

        _coroutine = null;
    }
    [SerializeField]
    private LiatrisPatternBullet _patternBullet;

    public void ResetBullet()
    {
        _floor.SetActive(true);
        _shpere.SetActive(true);
        GetComponent<Animator>().SetTrigger("Reset");
        transform.position = _prevPos.position;
        _isFire = false;

        _patternBullet.BulletOff();

        _currentCounter = 0;
    }

}
