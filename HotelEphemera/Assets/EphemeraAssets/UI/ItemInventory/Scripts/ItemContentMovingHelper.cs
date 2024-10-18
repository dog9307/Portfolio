using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemContentMovingHelper : MonoBehaviour
{
    [SerializeField]
    private Canvas _targetParentCanvas;
    private Transform _prevParent;
    [HideInInspector]
    [SerializeField]
    private Transform _targetPos;
    [SerializeField]
    private float _moveSpeed = 0.04f;

    void Start()
    {
        if (!_targetParentCanvas) return;

        _prevParent = transform.parent;
        transform.parent = _targetParentCanvas.transform;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPos.position, _moveSpeed);
    }

    public void ResetParent()
    {
        transform.parent = _prevParent;
    }
}
