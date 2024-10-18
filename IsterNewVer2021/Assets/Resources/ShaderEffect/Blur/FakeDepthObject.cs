using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeDepthObject : MonoBehaviour
{
    [SerializeField]
    private BlurController _blur;

    [SerializeField]
    private float _minDepth;
    [SerializeField]
    private float _maxDepth;

    [SerializeField]
    private float _minBlurAmount = 0.0f;
    [SerializeField]
    private float _maxBlurAmount = 10.0f;

    // Update is called once per frame
    void Update()
    {
        if (!_blur)
        {
            Destroy(this);
            return;
        }

        float value = Camera.main.orthographicSize;
        float start = _minDepth;
        float end = _maxDepth;

        float ratio = (value - start) / (end - start);
        ratio = Mathf.Clamp(ratio, 0.0f, 1.0f);

        _blur.currentAmount = Mathf.Lerp(_minBlurAmount, _maxBlurAmount, ratio);
    }
}
