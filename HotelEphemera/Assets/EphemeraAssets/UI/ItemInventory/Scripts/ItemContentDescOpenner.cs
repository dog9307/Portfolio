using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemContentDescOpenner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private static ItemContentDescPanel _descPanel;

    [HideInInspector]
    [SerializeField]
    private ItemContentBinder _targetBinder;

    [Header("Desc")]
    [SerializeField]
    private string _name;
    [TextArea(5, 10)]
    [SerializeField]
    private string _desc;

    #region InPC
    public void OnPointerEnter(PointerEventData eventData)
    {
        DescOpen();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DescClose();
    }
    #endregion

    //#region InMobile

    //private float _touchOverTime = 2.0f;
    //private float _currentTime = 0.0f;
    //private float _prevTime = 0.0f;

    //void OnMouseEnter()
    //{
    //    _currentTime = 0.0f;
    //    _prevTime = 0.0f;
    //}

    //void OnMouseOver()
    //{
    //    _currentTime += TimeManager.originDeltaTime;
    //    if (_prevTime < _touchOverTime && _currentTime >= _touchOverTime)
    //        DescOpen();
    //    _prevTime = _currentTime;
    //}

    //void OnMouseUp()
    //{
    //    _currentTime = 0.0f;
    //}

    //#endregion

    void DescOpen()
    {
        if (!_descPanel)
            _descPanel = FindObjectOfType<ItemContentDescPanel>();

        _descPanel.OpenPanel(_targetBinder.itemImage, _name, _desc);
    }

    void DescClose()
    {
        if (!_descPanel)
            _descPanel = FindObjectOfType<ItemContentDescPanel>();

        _descPanel.ClosePanel();
    }
}
