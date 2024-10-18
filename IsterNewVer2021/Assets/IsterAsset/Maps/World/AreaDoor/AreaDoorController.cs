using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDoorController : MonoBehaviour
{
    [SerializeField]
    private int _areaID;
    [SerializeField]
    private string _fieldName;
    [SerializeField]
    private int _startPosID;

    private static PlayerMapChanger _mapChanger;

    public void GoToNextArea()
    {
        if (!_mapChanger)
            _mapChanger = FindObjectOfType<PlayerMapChanger>();

        _mapChanger.ChangeArea(_areaID, _fieldName, _startPosID);
    }
}
