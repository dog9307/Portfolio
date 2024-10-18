using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerF1FlowerGuide : MonoBehaviour
{
    [SerializeField]
    private FlowerType _type;
    public FlowerType type { get { return _type; } }

    [SerializeField]
    private FadingGuideUI _fading;

    [SerializeField]
    private float _showTime = 5.0f;

    public bool isAlreadyShown { get; set; }

    public void ShowGuide()
    {
        if (isAlreadyShown) return;

        isAlreadyShown = true;
        StartCoroutine(Show());
    }

    IEnumerator Show()
    {
        _fading.StartFading(1.0f);
        yield return new WaitForSeconds(_fading.totalFadeTime + _showTime);
        _fading.StartFading(0.0f);
    }
}
