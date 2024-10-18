using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DragableIcon : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private RectTransform[] _destRects;
    [SerializeField]
    private Image _dragableSprite;
    public Vector2 sizeDelta { get { if (_dragableSprite) return _dragableSprite.rectTransform.sizeDelta; return Vector2.zero; } }

    [SerializeField]
    private DragableIconController _controller;

    public UnityEvent OnDragEnd;

    public int targetIndex { get; set; }

    [SerializeField]
    private bool _isYFlip = false;

    void OnEnable()
    {
        if (!_controller)
            _controller = FindObjectOfType<DragableIconController>();

        if (!_dragableSprite)
            _dragableSprite = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _controller.DragIconOn(_dragableSprite.sprite, this, _isYFlip);
    }

    public void EndDrag()
    {
        int invokeIndex = -1;
        for (int i = 0; i < _destRects.Length; ++i)
        {
            if (_destRects[i])
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(_destRects[i], Input.mousePosition))
                {
                    invokeIndex = i;
                    break;
                }
            }
        }
        
        if (invokeIndex != -1)
        {
            targetIndex = invokeIndex;
            if (OnDragEnd != null)
                OnDragEnd.Invoke();
        }

        _controller.DragIconOff();
    }
}
