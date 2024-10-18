using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObserve : MonoBehaviour
{
    public GameObject _roomObject;
    public Animator _observeAni;

    private bool _observeStart = false;
    public bool observeStart { get { return _observeStart; } set { _observeStart = value; } }

    public void ObserveRL()
    {
        _observeAni.SetTrigger("oberveRoomRL");
        _observeStart = true;
    }
    public void ObserveUD()
    {
        _observeAni.SetTrigger("oberveRoomUD");
        _observeStart = true;
    }
}
