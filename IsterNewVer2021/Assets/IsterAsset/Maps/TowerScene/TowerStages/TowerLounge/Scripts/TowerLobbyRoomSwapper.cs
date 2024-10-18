using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLobbyRoomSwapper : MonoBehaviour
{
    [SerializeField]
    private GameObject _lobby;
    [SerializeField]
    private GameObject _dummyRoom;

    private GameObject _currentRoom;
    private GameObject _nextRoom;

    public int nextStartPosID { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _currentRoom = _dummyRoom;
    }

    public void PlayerFreezing()
    {
        FindObjectOfType<PlayerMoveController>().PlayerMoveFreeze(true);
    }

    public void PlayerUnfreezing()
    {
        FindObjectOfType<PlayerMoveController>().PlayerMoveFreeze(false);
    }

    public void ChangeMap(GameObject nextMap, int startPosID, float fadeOutTime = 0.5f, float fadeInTime = 0.5f)
    {
        _nextRoom = nextMap;
        nextStartPosID = startPosID;

        BlackMaskController.instance.AddEvent(PlayerFreezing, BlackMaskEventType.PRE);
        BlackMaskController.instance.AddEvent(OpenMap, BlackMaskEventType.MIDDLE);
        BlackMaskController.instance.AddEvent(PlayerUnfreezing, BlackMaskEventType.POST);
        BlackMaskController.instance.StartFading(fadeOutTime, fadeInTime);
    }

    public void OpenMap()
    {
        _currentRoom.SetActive(false);
        _currentRoom = _nextRoom;
        _currentRoom.SetActive(true);

        Invoke("FindStartPos", Time.deltaTime);
    }

    public void FindStartPos()
    {
        MapStartPos[] poses = FindObjectsOfType<MapStartPos>();
        foreach (var pos in poses)
        {
            if (pos.id == nextStartPosID)
            {
                pos.PlayerStarting();
                break;
            }
        }
    }
}
