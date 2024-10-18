using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLobbyDoor : MonoBehaviour
{
    [SerializeField]
    private GameObject _toRoom;
    [SerializeField]
    private int _nextPosID;

    public void RoomChange()
    {
        TowerLobbyRoomSwapper swapper = FindObjectOfType<TowerLobbyRoomSwapper>();
        if (swapper)
            swapper.ChangeMap(_toRoom, _nextPosID);
    }
}
