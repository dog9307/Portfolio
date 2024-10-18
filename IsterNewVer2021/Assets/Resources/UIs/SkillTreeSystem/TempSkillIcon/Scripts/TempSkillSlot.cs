using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempSkillSlot : MonoBehaviour
{
    [SerializeField]
    private Image _firstImage;
    [SerializeField]
    private Image _skillIcon;
    [SerializeField]
    private Image _back;
    [SerializeField]
    private Text _skillCount;

    private SkillBase _skill;
    public SkillBase relativeSkill { get { return _skill; } }
    private SkillUser _user;

    private float _maxGuage;
    private float _currentGuage;

    [SerializeField]
    private Sprite _emptySlot;

    [SerializeField]
    private ParticleSystem _coolTimeEndEffect;

    [SerializeField]
    private SFXPlayer _sfx;
    [SerializeField]
    private string _sfxName = "coolTimeEnd";
    
    public void ChangeIcon(SkillBase skill, PlayerSkillUsage skillUsage, bool alphaChange = true)
    {
        _skill = skill;
        if (skill != null)
        {
            _skillIcon.sprite = _skill.skillIcon;
            _user = skillUsage.FindUser(skill.GetType());

            if (alphaChange)
            {
                Color color;
                color = _skillIcon.color;
                color.a = 1.0f;
                _skillIcon.color = color;

                if (_back)
                {
                    color = _back.color;
                    color.a = 1.0f;
                    _back.color = color;
                }

                if (_firstImage)
                {
                    color = _firstImage.color;
                    color.a = 0.7f;
                    _firstImage.color = color;
                }
            }
        }
        else
        {
            _skillIcon.sprite = _emptySlot;
            _user = null;
            float alpha = 0.25f;

            if (alphaChange)
            {
                Color color;
                color = _skillIcon.color;
                color.a = alpha;
                _skillIcon.color = color;

                if (_back)
                {
                    color = _back.color;
                    color.a = alpha;
                    _back.color = color;
                }

                if (_firstImage)
                {

                    color = _firstImage.color;
                    color.a = alpha;
                    _firstImage.color = color;
                }
            }
        }

        if (_user)
        {
            if (_skillCount)
                _skillCount.gameObject.SetActive(typeof(CountableUserBase).IsInstanceOfType(_user));
        }
        else
            _skillCount.gameObject.SetActive(false);

        _firstImage.sprite = _skillIcon.sprite;
    }

    void Update()
    {
        CoolTimeUpdate();
    }

    private void CoolTimeUpdate()
    {
        if (_user)
        {
            if (typeof(ICoolTime).IsInstanceOfType(_user))
            {
                _maxGuage = ((ICoolTime)_user).totalCoolTime;
                _currentGuage = ((ICoolTime)_user).currentCoolTime;
            }
            else
            {
                _maxGuage = 1.0f;
                _currentGuage = 1.0f;
            }

            if (typeof(CountableUserBase).IsInstanceOfType(_user))
            {
                CountableUserBase user = (CountableUserBase)_user;
                _skillCount.text = user.currentCount.ToString();
            }
        }
        else
        {
            _maxGuage = 1.0f;
            _currentGuage = 1.0f;
        }

        float prevAmount = _firstImage.fillAmount;
        _firstImage.fillAmount = (_maxGuage - _currentGuage) / _maxGuage;
        if (prevAmount > 0.0f &&
            _firstImage.fillAmount <= 0.0f)
        {
            if (_coolTimeEndEffect)
                _coolTimeEndEffect.Play();

            if (_sfx)
                _sfx.PlaySFX(_sfxName);
        }
    }

    private int _coroutineCount = 0;
    private float _currentScaleTime = 0.0f;
    private float _totalScaleTime = 0.15f;
    private float _currentScaleFactor = 1.0f;
    public void ScaleChange(float toScale)
    {
        if (gameObject.activeSelf)
            StartCoroutine(Scaling(toScale));
    }

    IEnumerator Scaling(float toScale)
    {
        _coroutineCount++;
        _currentScaleTime = _totalScaleTime - _currentScaleTime;
        float startScaleFactor = _currentScaleFactor;
        while (_currentScaleTime < _totalScaleTime)
        {
            _currentScaleTime += IsterTimeManager.deltaTime;
            float ratio = _currentScaleTime / _totalScaleTime;

            _currentScaleFactor = Mathf.Lerp(startScaleFactor, toScale, ratio);
            transform.localScale = new Vector3(_currentScaleFactor, _currentScaleFactor, 1.0f);

            yield return null;

            if (_coroutineCount > 1)
                break;
        }
        _currentScaleTime = _totalScaleTime;

        _currentScaleFactor = Mathf.Lerp(startScaleFactor, toScale, 1.0f);
        transform.localScale = new Vector3(_currentScaleFactor, _currentScaleFactor, 1.0f);

        _coroutineCount--;
    }
}
