using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public enum TeleportSceneType
{
    NONE = -1,
    World,
    Tower,
    END
}

public class MinimapTeleportButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [HideInInspector]
    [SerializeField]
    private FadingGuideUI _desc;

    [SerializeField]
    private TeleportSceneType _type;
    [SerializeField]
    private string _toFieldName;
    [SerializeField]
    private AreaID _areaID;
    [SerializeField]
    private TowerID _towerID;
    [SerializeField]
    private int _startPosID;

    [SerializeField]
    private int _buttonID;
    public int buttonID { get { return _buttonID; } }

    private static PlayerMapChanger _mapChanger;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_desc) return;

        _desc.StartFading(1.0f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.instance.tabOn = false;

        if (!_mapChanger)
            _mapChanger = FindObjectOfType<PlayerMapChanger>();

        switch (_type)
        {
            case TeleportSceneType.World:
                _mapChanger.MinimapTeleport(_type, (int)_areaID, _toFieldName, _startPosID);
            break;

            case TeleportSceneType.Tower:
                _mapChanger.MinimapTeleport(_type, (int)_towerID, _toFieldName, _startPosID);
            break;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_desc) return;

        _desc.StartFading(0.0f);
    }
}
