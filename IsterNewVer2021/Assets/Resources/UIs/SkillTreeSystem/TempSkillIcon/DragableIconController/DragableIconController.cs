using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragableIconController : MonoBehaviour
{
    [SerializeField]
    private Image _dragIcon;

    public DragableIcon currentIcon { get; set; }

    public List<DragDestination> destList { get; set; } = new List<DragDestination>();

    private void OnEnable()
    {
        DragIconOff();
    }

    void Update()
    {
        if (!_dragIcon.gameObject.activeSelf) return;

        _dragIcon.rectTransform.position = Input.mousePosition;

        if (!Input.GetMouseButton(0))
            currentIcon.EndDrag();
    }

    public void DragIconOn(Sprite sprite, DragableIcon current, bool isYFlip)
    {
        _dragIcon.gameObject.SetActive(true);
        _dragIcon.sprite = sprite;

        _dragIcon.rectTransform.sizeDelta = current.sizeDelta;

        if (isYFlip)
        {
            Vector3 scale = _dragIcon.rectTransform.localScale;
            scale.y *= -1.0f;
            _dragIcon.rectTransform.localScale = scale;
        }

        currentIcon = current;
    }

    public void DragIconOff()
    {
        currentIcon = null;
        _dragIcon.gameObject.SetActive(false);
    }
}
