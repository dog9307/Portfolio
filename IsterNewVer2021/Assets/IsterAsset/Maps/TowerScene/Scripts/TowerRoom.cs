using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRoom : MonoBehaviour
{
    [SerializeField]
    private TowerDoorManager _doorManager;
    [SerializeField]
    private bool _isStartRoom = false;

    [SerializeField]
    private bool _isBossReadyRoom = false;
    public bool isBossReadyRoom { get { return _isBossReadyRoom; } }

    // Start is called before the first frame update
    void Awake()
    {
        _doorManager.room = this;
    }

    void Start()
    {
        if (_isStartRoom && !_isBossReadyRoom)
        {
            StageBGMPlayer bgm = FindObjectOfType<StageBGMPlayer>();
            if (bgm)
                bgm.PlayBGM();
        }
    }

    public TowerDoor GetRandomDoor()
    {
        TowerDoor rndDoor = _doorManager.GetRandomDoor(isBossReadyRoom);
        return rndDoor;
    }

    public void OpenDoors()
    {
        _doorManager.OpenAllDoors();
    }

    public void SetToNormalRoom()
    {
        _doorManager.SetToNormalDoor();
    }

    public void SetToBossRoom()
    {
        _doorManager.SetToBossDoor();
    }
}
