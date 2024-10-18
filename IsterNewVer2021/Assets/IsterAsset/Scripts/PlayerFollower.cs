using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    private PlayerMoveController _player;

    [SerializeField]
    private Vector3 _offset;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerMoveController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();

        if (!_player) return;

        Vector3 newPos = _player.transform.position;
        transform.position = newPos + _offset;
    }
}
