using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTailScaler : MonoBehaviour
{
    [SerializeField]
    private Transform _targetScale;

    // Update is called once per frame
    void Update()
    {
        if (!_targetScale) return;

        Vector3 scale = transform.localScale;
        scale.x = _targetScale.transform.localScale.y;
        transform.localScale = scale;
    }
}
