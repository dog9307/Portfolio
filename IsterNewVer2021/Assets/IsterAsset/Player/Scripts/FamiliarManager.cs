using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamiliarManager : MonoBehaviour
{
    private PlayerMoveController _player;
    private BuffInfo _playerBuff;

    [SerializeField]
    private float _moveRange;
    public float moveRange { get { return _moveRange - _playerBuff.moveRangeDecrease; } set { _moveRange = value; } }

    private List<FamiliarMoveController> _familiars = new List<FamiliarMoveController>();

    void Start()
    {
        _player = FindObjectOfType<PlayerMoveController>();
        _playerBuff = _player.GetComponent<BuffInfo>();
    }

    public void AddFamiliar(FamiliarMoveController familiar)
    {
        _familiars.Add(familiar);
    }
    
    public void ReleaseAll()
    {
        foreach (FamiliarMoveController fam in _familiars)
            fam.Release();

        _familiars.Clear();
    }


    public Vector2 RandomPos()
    {
        Vector2 pos = _player.center;

        float angle = Random.Range(0.0f, 360.0f);
        float distance = Random.Range(2.0f, moveRange);

        pos += new Vector2(Mathf.Cos(angle) * distance, Mathf.Sin(angle) * distance);

        return pos;
    }
}
