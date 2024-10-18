using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovable : Movable
{
    [SerializeField]
    BossMain _bossMain;
    [SerializeField]
    BossController _controller;

    [SerializeField]
    List<BossMoveBase> _moveList = new List<BossMoveBase>();
    public List<BossMoveBase> moveList { get { return _moveList; } }

   //[SerializeField]
   //List<BossMoveBase> _moveSecondList = new List<BossMoveBase>();
   //public List<BossMoveBase> moveSecondList { get { return _moveSecondList; } }

    public bool _moveStart;
    public bool _moveEnd;

    // Start is called before the first frame update
    void Start()
    {
        _moveEnd = false;
        _moveStart = false;

    }
    protected override void ComputeVelocity()
    {

    }
    public void MoveReset()
    {
        _moveStart =false;
        _moveEnd = false;
    }
}
