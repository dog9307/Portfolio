using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDoorManager : MonoBehaviour
{
    private List<TowerDoor> _doors = new List<TowerDoor>();

    public TowerRoom room { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        TowerDoor[] doors = GetComponentsInChildren<TowerDoor>();
        foreach (var door in doors)
        {
            door.manager = this;
            _doors.Add(door);
        }
    }

    private void OnEnable()
    {
        MoveRoomBlackMask.instance.AddEvent(CloseAllDoors, BlackMaskEventType.POST);
    }

    public TowerDoor GetRandomDoor(bool isBossReadyRoom)
    {
        TowerDoor rndDoor = null;

        if (isBossReadyRoom)
        {
            foreach (var d in _doors)
            {
                if (d.startPosID == 0)
                {
                    rndDoor = d;
                    break;
                }
            }
        }
        else
        {
            if (_doors.Count >= 1)
            {
                int rnd = Random.Range(0, _doors.Count);
                rndDoor = _doors[rnd];
            }
        }

        return rndDoor;
    }

    //void Update()
    //{
    //    // test
    //    if (Input.GetKeyDown(KeyCode.F9))
    //        OpenAllDoors();

    //    if (Input.GetKeyDown(KeyCode.F10))
    //        CloseAllDoors();
    //}

    public void OpenAllDoors()
    {
        foreach (var door in _doors)
            door.OpenDoor();
    }

    public void CloseAllDoors()
    {
        foreach (var door in _doors)
            door.CloseDoor();
    }

    public void SetToNormalDoor()
    {
        foreach (var door in _doors)
            door.SetToNormalDoor();
    }

    public void SetToBossDoor()
    {
        foreach (var door in _doors)
            door.SetToBossDoor();
    }
}
