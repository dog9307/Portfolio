using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldTimeAttackProgressBar : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private FieldTimeAttackController _timer;

    [HideInInspector]
    [SerializeField]
    private Image _front;
    [HideInInspector]
    [SerializeField]
    private Image _additional;
    private Vector3 _startPos;

    [HideInInspector]
    [SerializeField]
    private FadingGuideUI _fading;

    void Start()
    {
        _startPos = _additional.rectTransform.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_timer.isTimerOn) return;

        float ratio = (1.0f - _timer.ratio);

        _front.fillAmount = ratio;

        Vector3 newPos = _additional.rectTransform.anchoredPosition;
        newPos.x = Mathf.Lerp(-_startPos.x, _startPos.x, ratio);
        _additional.rectTransform.anchoredPosition = newPos;
    }

    public void Fade(float toFade)
    {
        _fading.StartFading(toFade);
    }
}
