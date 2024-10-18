using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDestination : MonoBehaviour
{
    private DragableIconController _controller;

    private void OnEnable()
    {
        if (!_controller)
            _controller = FindObjectOfType<DragableIconController>();
    }
}
