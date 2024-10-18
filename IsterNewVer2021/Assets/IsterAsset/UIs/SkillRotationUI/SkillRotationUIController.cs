using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SkillRotUIDir
{
    NONE,
    LEFT = -1,
    RIGHT = 1
}

public class SkillRotationUIController : MonoBehaviour
{
    [SerializeField]
    private List<TempSkillSlot> _skillSlots = new List<TempSkillSlot>();
    private int _currentIndex;

    private PlayerSkillUsage _playerSkill;

    [SerializeField]
    private float _rad = 2.5f;
    private float _currentRad;
    private float _currentTime = 0.0f;
    private float _totalTime = 0.15f;
    private float _currentAngle = 0.0f;
    private float _toAngle = 0.0f;
    private int _coroutineCount = 0;

    [SerializeField]
    private Graphic[] _relativeImages;
    [SerializeField]
    private Image _back;

    [SerializeField]
    private SFXPlayer _sfx;

    private EventTimer _timer;
    void Start()
    {
        _toAngle = _currentAngle;
        _currentRad = 0.0f;

        _timer = GetComponent<EventTimer>();
        _timer.AddEvent(Disappear);

        _isAppear = true;
        _currentAppearTime = _totalAppearTime;
        Disappear();
    }

    void Update()
    {
        if (!_playerSkill)
            _playerSkill = FindObjectOfType<PlayerSkillUsage>();

        if (!_playerSkill) return;

        int count = _playerSkill.activeSkills.Count;
        float startAngle = _currentAngle;
        for (int i = 0; i < count; ++i)
        {
            float angle = startAngle - (360.0f / count) * i;
            float x = Mathf.Sin(angle * Mathf.Deg2Rad) * _currentRad;
            float y = Mathf.Cos(angle * Mathf.Deg2Rad) * _currentRad;

            _skillSlots[i].transform.localPosition = new Vector3(x, y, 0.0f);
        }
    }

    //private int _appearCount = 0;
    private float _currentAppearRatio = 0.0f;
    private float _currentAppearTime = 0.0f;
    private float _totalAppearTime = 0.15f;
    private bool _isAppear;
    private Coroutine _appearCoroutine;
    public void Appear()
    {
        if (_timer)
            _timer.TimerStart();

        if (_isAppear) return;

        _timer.AddEvent(Disappear);
        _isAppear = true;

        if (_appearCoroutine != null)
        {
            StopCoroutine(_appearCoroutine);
            _appearCoroutine = null;
        }

        _appearCoroutine = StartCoroutine(Appear(1.0f));
    }

    public void Disappear()
    {
        if (!_isAppear) return;

        _isAppear = false;

        if (_appearCoroutine != null)
        {
            StopCoroutine(_appearCoroutine);
            _appearCoroutine = null;
        }

        _appearCoroutine = StartCoroutine(Appear(0.0f));
    }

    IEnumerator Appear(float toRatio)
    {
        yield return new WaitForEndOfFrame();
        //_appearCount++;
        _currentAppearTime = _totalAppearTime - _currentAppearTime;
        float startRatio = _currentAppearRatio;
        Color color;
        while (_currentAppearTime < _totalAppearTime)
        {
            _currentAppearTime += IsterTimeManager.deltaTime;
            float ratio = _currentAppearTime / _totalAppearTime;

            _currentAppearRatio = Mathf.Lerp(startRatio, toRatio, ratio);
            foreach (var img in _relativeImages)
            {
                color = img.color;

                if (img.transform.name.Contains("CoolTime"))
                    color.a = _currentAppearRatio * 0.7f;
                else
                    color.a = _currentAppearRatio;
                img.color = color;
            }

            color = _back.color;
            color.a = _currentAppearRatio;
            _back.color = color;

            _back.transform.localScale = new Vector3(_currentAppearRatio, _currentAppearRatio, 0.0f);

            _currentRad = Mathf.Lerp(0.0f, _rad, _currentAppearRatio);

            yield return null;

            //if (_appearCount > 1)
            //{
            //    _appearCount--;
            //    break;
            //}
        }
        _currentAppearRatio = toRatio;
        foreach (var img in _relativeImages)
        {
            color = img.color;

            if (img.transform.name.Contains("CoolTime"))
                color.a = _currentAppearRatio * 0.7f;
            else
                color.a = _currentAppearRatio;
            img.color = color;
        }

        color = _back.color;
        color.a = _currentAppearRatio;
        _back.color = color;

        _back.transform.localScale = new Vector3(_currentAppearRatio, _currentAppearRatio, 0.0f);

        _currentRad = Mathf.Lerp(0.0f, _rad, _currentAppearRatio);

        _appearCoroutine = null;

        //_appearCount--;
    }

    public void ChangeIcons(List<TempSkillSlot> slots)
    {
        if (!_playerSkill)
            _playerSkill = FindObjectOfType<PlayerSkillUsage>();

        for (int i = 0; i < slots.Count; ++i)
            slots[i].gameObject.SetActive(false);

        for (int i = 0; i < _playerSkill.activeSkills.Count; ++i)
        {
            slots[i].gameObject.SetActive(true);

            _skillSlots[i].gameObject.SetActive(true);
            _skillSlots[i].ChangeIcon(slots[i].relativeSkill, _playerSkill, false);
        }

        SkillBase currentSkill = _playerSkill.currentSelectSkill;
        UpdateUI(currentSkill);
    }

    public void ChangeIcon(SkillBase newSkill, int targetIndex)
    {
        if (!_playerSkill)
            _playerSkill = FindObjectOfType<PlayerSkillUsage>();

        _skillSlots[targetIndex].gameObject.SetActive(true);
        _skillSlots[targetIndex].ChangeIcon(newSkill, _playerSkill, false);

        SkillBase currentSkill = _playerSkill.currentSelectSkill;
        UpdateUI(currentSkill);
    }

    public void UpdateUI(SkillRotUIDir dir)
    {
        if (_playerSkill.activeSkills.Count <= 0) return;

        if (_sfx)
            _sfx.PlaySFX("scroll");

        int maximumCount = _playerSkill.activeSkills.Count;
        int rotCount = 0;
        if (dir == SkillRotUIDir.LEFT)
        {
            do
            {
                rotCount++;
                _currentIndex = (_currentIndex + _playerSkill.activeSkills.Count - 1) % _playerSkill.activeSkills.Count;
            } while (_skillSlots[_currentIndex].relativeSkill == null && rotCount < maximumCount);
        }
        else if (dir == SkillRotUIDir.RIGHT)
        {
            do
            {
                rotCount++;
                _currentIndex = (_currentIndex + 1) % _playerSkill.activeSkills.Count;
            } while (_skillSlots[_currentIndex].relativeSkill == null && rotCount < maximumCount);
        }

        if (_skillSlots[_currentIndex].relativeSkill == null) return;

        float angleAddition = (360.0f / _playerSkill.activeSkills.Count) * (float)dir * rotCount;
        _toAngle += angleAddition;
        StartCoroutine(Rotating(_toAngle));

        SkillBase currentSkill = _playerSkill.currentSelectSkill;
        UpdateUI(currentSkill);

        Appear();
    }

    private float _selectedScale = 1.3f;
    private float _normalScale = 1.0f;
    public void UpdateUI(SkillBase currentSelectedSkill)
    {
        foreach (var slot in _skillSlots)
        {
            if (currentSelectedSkill == null)
            {
                slot.ScaleChange(_normalScale);
                continue;
            }

            if (slot.relativeSkill == null)
                slot.ScaleChange(_normalScale);
            else
            {
                if (slot.relativeSkill == currentSelectedSkill)
                    slot.ScaleChange(_selectedScale);
                else
                    slot.ScaleChange(_normalScale);
            }
        }
    }

    IEnumerator Rotating(float toAngle)
    {
        _coroutineCount++;
        _currentTime = 0.0f;
        float startAngle = _currentAngle;
        while (_currentTime < _totalTime)
        {
            float ratio = _currentTime / _totalTime;
            _currentAngle = Mathf.Lerp(startAngle, toAngle, ratio);

            yield return null;

            _currentTime += IsterTimeManager.deltaTime;

            if (_coroutineCount > 1)
            {
                _coroutineCount--;
                yield break;
            }
        }
        _currentAngle = toAngle;

        while (_currentAngle > 360.0f)
            _currentAngle -= 360.0f;
        while (_currentAngle < 0.0f)
            _currentAngle += 360.0f;

        _toAngle = _currentAngle;

        _coroutineCount--;
    }
}
