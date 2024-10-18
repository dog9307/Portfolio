using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacker : MonoBehaviour
{
    [SerializeField]
    BossMain _bossMain;
    [SerializeField]
    BossController _controller;

    [SerializeField]
    List<BossAttackBase> _attackList = new List<BossAttackBase>();
    public List<BossAttackBase> attackList { get { return _attackList; } }

    [SerializeField]
    List<BossAttackBase> _attackSecondList = new List<BossAttackBase>();
    public List<BossAttackBase> attackSecondList  { get { return _attackSecondList; } }

    [SerializeField]
    List<BossAttackBase> _meleeAttackList = new List<BossAttackBase>();
    public List<BossAttackBase> meleeAttackList { get { return _meleeAttackList; } }

    public bool _attackStart;
    public bool _attackEnd;
    // Start is called before the first frame update
    void Start()
    {
        _attackStart =false;
        _attackEnd =false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackReset()
    {
        _attackStart = false;
        _attackEnd = false;
    }
   // public void ListChange()
   // {
   //     for (int i = 0; i < _attackSecondList.Count; i++)
   //     {
   //         _attackList.Add(_attackSecondList[i]);
   //     }
   //     _attackSecondList.Clear();
   // }
    
}
