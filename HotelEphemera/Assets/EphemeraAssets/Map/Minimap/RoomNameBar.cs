using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomNameBar : MonoBehaviour
{
    //룸 이름바 관련
    public TextMeshProUGUI _roomName;

    Coroutine _roomNameBar;

    //nameBar
    public Image _nameBarR;
    public Image _nameBarL;

    public float _fillTime;
    float _currntFillTime;

    //nameText
    public float _appearTime;
    float _currentAppearTime;


    // Start is called before the first frame update

    private void OnEnable()
    {
        _currntFillTime = 0;
        _currentAppearTime = 0;
        _roomNameBar = StartCoroutine(NameBarActive());
    }
    private void OnDisable()
    {
        _roomName.text = null;
        _roomName.gameObject.SetActive(false);
        StopCoroutine(_roomNameBar);
    }
    public IEnumerator NameBarActive()
    {
        float fillAmount = 0;
        fillAmount = Mathf.Lerp(0.0f,1.0f, 0.0f);

        while (_currntFillTime < _fillTime)
        {
            _currntFillTime += Time.deltaTime;

            float ratio = _currntFillTime / _fillTime;

            fillAmount = Mathf.Lerp(0.0f, 1.0f, ratio);

            _nameBarR.fillAmount = fillAmount;
            _nameBarL.fillAmount = fillAmount;

            yield return null;
        }

        if(_roomName) _roomName.gameObject.SetActive(true);

        _roomNameBar = StartCoroutine(NameTextActive());

        yield return null;
    }
    public IEnumerator NameTextActive()
    {
        Color color = _roomName.color;
        color.a = Mathf.Lerp(0.0f, 1.0f, 0.0f);

        while (_currentAppearTime < _appearTime)
        {
            _currentAppearTime += Time.deltaTime;

            float ratio = _currentAppearTime / _appearTime;

            color.a = Mathf.Lerp(0.0f,1.0f, ratio);

            _roomName.color = color;

            yield return null;
        }

        StopCoroutine(_roomNameBar);

        yield return null;
    }
}
