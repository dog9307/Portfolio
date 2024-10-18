using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalDoor : DisposableObject
{
    [SerializeField]
    private ConditionalDoorTalkFrom _talkFrom;
    [SerializeField]
    private Collider2D _relativeCol;

    // test
    [SerializeField]
    private GameObject _doors;
    [SerializeField]
    private Collider2D _col;
    [SerializeField]
    private string _relativeKey = "";
    private void OnEnable()
    {
        if (_relativeKey.Length <= 0) return;

        if (_doors)
        {
            int count = PlayerPrefs.GetInt(_relativeKey, 0);
            if (count >= 100)
            {
                _doors.SetActive(false);
                Destroy(_col);
                return;
            }

            count = SavableDataManager.instance.FindIntSavableData(_relativeKey);
            if (count >= 100)
            {
                _doors.SetActive(false);
                Destroy(_col);
            }
        }
    }

    public override void AlreadyUsed()
    {
        base.AlreadyUsed();

        _talkFrom.AlreadyUsed();

        Destroy(_talkFrom);
        Destroy(_relativeCol);
    }

    public override void UseObject()
    {
        base.UseObject();

        Destroy(_talkFrom);
    }

    public void ColliderOff()
    {
        Destroy(_relativeCol);
    }
}
