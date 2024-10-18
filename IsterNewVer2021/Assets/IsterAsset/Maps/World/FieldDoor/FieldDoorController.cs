using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldDoorController : MonoBehaviour
{
    [SerializeField]
    private GameObject _nextField;
    [SerializeField]
    private MapStartPos _nextStartPos;

    private static PlayerMapChanger _mapChanger;

    public void GoToNextField()
    {
        if (!_mapChanger)
            _mapChanger = FindObjectOfType<PlayerMapChanger>();

        _mapChanger.ChangeField(_nextField, _nextStartPos.id);
    }
}
