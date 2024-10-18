using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRoomSetter : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _roomPrefabs = new List<GameObject>();

    [SerializeField]
    private GameObject _startRoomPrefab;
    [SerializeField]
    private GameObject _bossRoomPrefab;
    private GameObject _bossRoom;

    [SerializeField]
    private GameObject _endRoom;

    [SerializeField]
    private int _roomCount = 12;
    private int _currentRoom = 0;

    private List<GameObject> _rooms = new List<GameObject>();

    void Start()
    {
        CreateRooms();

        _currentRoom = 0;
        OpenNextRoom();
    }

    void CreateRooms()
    {
        // 원래 쓸 것
        //for (int i = 0; i < _roomCount; ++i)
        //{
        //    int rnd = Random.Range(0, _roomPrefabs.Count);
        //    if (!_roomPrefabs[rnd])
        //    {
        //        i--;
        //        continue;
        //    }

        //    GameObject newRoom = Instantiate(_roomPrefabs[rnd]);
        //    newRoom.SetActive(false);

        //    _rooms.Add(newRoom);
        //}

        // 데모용
        //for (int i = 0; i < _roomPrefabs.Count; ++i)
        //{
        //    GameObject newRoom = Instantiate(_roomPrefabs[i]);
        //    newRoom.SetActive(false);

        //    _rooms.Add(newRoom);
        //}
        //CommonFuncs.ShuffleList<GameObject>(_rooms);

        if (_startRoomPrefab)
        {
            GameObject newRoom = Instantiate(_startRoomPrefab);
            _rooms.Insert(0, newRoom);
        }

        if (_bossRoomPrefab)
        {
            _bossRoom = Instantiate(_bossRoomPrefab);
            _bossRoom.SetActive(false);
        }

        if (_endRoom)
        {
            GameObject newRoom = Instantiate(_endRoom);
            _rooms.Add(newRoom);
            newRoom.SetActive(false);
        }

        for (int i = 0; i < _rooms.Count - 1; ++i)
            _rooms[i].GetComponent<TowerRoom>().SetToNormalRoom();
        _rooms[_rooms.Count - 1].GetComponent<TowerRoom>().SetToBossRoom();
    }

    public void CloseCurrentRoom()
    {
        _rooms[_currentRoom - 1].SetActive(false);
    }

    public TowerRoom OpenNextRoom()
    {
        TowerRoom room = null;

        _currentRoom++;
        if (_currentRoom <= _rooms.Count)
        {
            room = _rooms[_currentRoom - 1].GetComponent<TowerRoom>();
            _rooms[_currentRoom - 1].SetActive(true);
        }
        else
        {
            if (_bossRoom)
                _bossRoom.SetActive(true);
        }

        return room;
    }
}
