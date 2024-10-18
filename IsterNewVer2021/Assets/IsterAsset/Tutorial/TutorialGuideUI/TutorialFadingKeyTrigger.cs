using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialFadingKeyTrigger : MonoBehaviour
{
    [SerializeField]
    private FadingGuideUI _fading;
    private bool _isFadingStart = false;
    public bool isFadingStart { get { return _isFadingStart; } }
    [SerializeField]
    private KeyCode[] _keys;

    public bool isEnable { get; set; } = false;

    public UnityEvent OnTriggerKey;
    public UnityEvent OnTriggerKeyAllDone;

    [SerializeField]
    private int _requireKeyCount = 1;
    private int _currentKeyCount = 0;

    [SerializeField]
    private bool _isAllDoneDelay = false;
    [SerializeField]
    private float _delayTime = 1.0f;

    void Start()
    {
        _currentKeyCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnable) return;
        if (_isFadingStart) return;

        int prevCount = _currentKeyCount;
        foreach (var key in _keys)
        {
            if (key == KeyCode.Mouse2)
            {
                if (Mathf.Abs(Input.mouseScrollDelta.magnitude) > 0)
                {
                    _currentKeyCount++;

                    if (OnTriggerKey != null)
                        OnTriggerKey.Invoke();
                }
            }
            else
            {
                if (Input.GetKeyDown(key))
                {
                    _currentKeyCount++;

                    if (OnTriggerKey != null)
                        OnTriggerKey.Invoke();

                    break;
                }
            }
        }

        if (prevCount < _requireKeyCount &&
            _currentKeyCount >= _requireKeyCount)
        {
            isEnable = false;
            _isFadingStart = true;

            if (_fading)
                _fading.StartFading(0.0f);

            if (!_isAllDoneDelay)
            {
                if (OnTriggerKeyAllDone != null)
                    OnTriggerKeyAllDone.Invoke();
            }
            else
                StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delayTime);

        if (OnTriggerKeyAllDone != null)
            OnTriggerKeyAllDone.Invoke();
    }
}
