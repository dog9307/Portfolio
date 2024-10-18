using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRepositionHelper : MonoBehaviour
{
    [SerializeField]
    private Vector2 _offset;

    void Start()
    {
        if (!transform.parent) return;

        transform.position = transform.parent.position + (Vector3)_offset * transform.lossyScale.y;
    }
}
