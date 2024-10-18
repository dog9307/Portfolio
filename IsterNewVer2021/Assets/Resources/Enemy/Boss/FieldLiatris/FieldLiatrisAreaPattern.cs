using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldLiatrisAreaPattern : MonoBehaviour
{
    [SerializeField]
    FieldLiatrisController _liatris; 

    [SerializeField]
    GameObject _object;

    [SerializeField]
    public List<GameObject> _objects = new List<GameObject>();

    [SerializeField]
    List<Transform> _objectPos;

    [SerializeField]
    GameObject _attackAreaReady;
    [SerializeField]
    Transform _attackAreaPos;
    [SerializeField]
    GameObject _attackArea;

    public float _objectCreateCount;
    private float _currentCount;

    public float _objectCreateTimer;
    private float _currentTimer;

    private bool _patternActive;

    private bool _specialPatternOn;
    Coroutine _currentCoroutine;

    public float _patternResetTimer;
    private float _currentResetTimer;

    [SerializeField]
    SFXPlayer _sfx;
    // Start is called before the first frame update
    void Start()
    {
        _patternActive = false;
        _currentCount = 0;
        _currentTimer = 0;
        _currentResetTimer = 0;
    }

    //// Update is called once per frame
    void Update()
    {
        if (_liatris._isActive && !_liatris._SpecialPatternEnd)
        {
            if (!_liatris._grogi)
            {
                if (_liatris._SpecialPatternStart && !_patternActive)
                {
                    _currentCoroutine = null;
                    _currentCoroutine = StartCoroutine(ObjectSpawn());
                }
            }
            else
            {
                _patternActive = false;
                if (_currentCoroutine != null)
                {
                    StopCoroutine(_currentCoroutine);
                }
                _currentCoroutine = null;
            }
        }
    }
    //
    void CreateObject()
    {
        if (_objectPos.Count > 0)
        {
            _sfx.PlaySFX("create_object");
            Transform pos = _objectPos[Random.Range(0, _objectPos.Count)].transform;
            GameObject obj = Instantiate(_object) as GameObject;
            obj.transform.position = pos.position;
            obj.GetComponent<Damagable>().isCanHurt = true;
            _objects.Add(obj);
        }
    }
    void CreateAttackReady()
    {
        GameObject obj = Instantiate(_attackAreaReady) as GameObject;
        obj.transform.position = _attackAreaPos.position;
        _attackAreaReady.gameObject.SetActive(true);
    }
    private void ResetPattern()
    {
        //_liatris._SpecialPatternEnd = true;
        if (_objects.Count > 0)
        {
            for (int i = _objects.Count-1; i >= 0; i--)
            {
                GameObject obj = _objects[i].gameObject;
                obj.GetComponent<SafetyZoneTrigger>().SafetyZoneOff();
                Destroy(obj);
                _objects.RemoveAt(i);
                _currentCount--;
            }
        }
        _currentCount=0;
        _currentCoroutine = null;
        _currentCoroutine = StartCoroutine(CoolTime());
    }
    public void RemoveObject(GameObject obj)
    {
        _objects.Remove(obj);
        _currentCount--;
    }
    public void DestroyObject(GameObject obj)
    {
        Destroy(obj);
    }
    //광역 공격 오브젝트 소환
    IEnumerator ObjectSpawn()
    {
        _patternActive = true;

        while (_currentCount < _objectCreateCount)
        {
            while (_currentTimer < _objectCreateTimer)
            {
                _currentTimer += IsterTimeManager.bossDeltaTime;
                yield return null;
            }
            _currentTimer = 0;

            _currentCount++;

            CreateObject();

            yield return null;

        }
        CreateAttackReady();

        _currentCoroutine = null;
        //세이프티 존 온, 밑 조건 판별.
        _sfx.PlaySFX("area_shield");
        _currentCoroutine = StartCoroutine(AreaAttackOn());

    }
    IEnumerator AreaAttackOn()
    {

        _sfx.PlaySFX("area_attack");
        for (int i = 0; i < _objects.Count; i++)
        {
             _objects[i].GetComponent<Damagable>().isCanHurt = false;
             _objects[i].GetComponent<SafetyZoneTrigger>().SafetyZoneOn();
        }


        while (!_liatris._SpecialPatternEnd)
            yield return null;

        ResetPattern();

    }

    IEnumerator CoolTime()
    {
        while (_currentResetTimer < _patternResetTimer)
        {
            _currentResetTimer += IsterTimeManager.bossDeltaTime;
            yield return null;
        }

        _patternActive = false;

        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }
    }
}
