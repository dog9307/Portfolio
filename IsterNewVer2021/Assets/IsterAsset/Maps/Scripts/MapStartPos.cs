using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapStartPos : MonoBehaviour
{
    [SerializeField]
    private int _startPosID;
    public int id { get { return _startPosID; } }

    [SerializeField]
    private bool _isPlayerMovingInStart = false;

    [SerializeField]
    private MoveRoomDir _dir;
    public MoveRoomDir dir { get { return _dir; } }

    void Start()
    {
        if (_isPlayerMovingInStart)
            PlayerStarting();
    }

    // Start is called before the first frame update
    public void PlayerStarting()
    {
        PlayerMoveController player = FindObjectOfType<PlayerMoveController>();
        if (player)
            player.Move(transform.position);
    }
}
