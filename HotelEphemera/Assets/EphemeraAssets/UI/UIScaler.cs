using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UIScaler : MonoBehaviour
{
    private void Awake()
    {
        ScaleChange();
    }

    private void ScaleChange()
    {
        Vector3 scale = transform.lossyScale;
        scale.x = 1.0f / scale.x;
        scale.y = 1.0f / scale.y;
        scale.z = 1.0f;
        transform.localScale = scale;
    }
}
