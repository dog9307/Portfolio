using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    [SerializeField]
    private bool _isCanCounterAttacked = true;
    public bool isCanCounterAttacked { get { return _isCanCounterAttacked; } set { _isCanCounterAttacked = value; } }

    [SerializeField]
    private bool _isAffectedGravity = true;
    public bool isAffectedGravity { get { return _isAffectedGravity; } set { _isAffectedGravity = value; } }

    [SerializeField]
    private bool _isAffectedTimeSlow = true;
    public bool isAffectedTimeSlow { get { return _isAffectedTimeSlow; } set { _isAffectedTimeSlow = value; } }
    [SerializeField]
    private bool _isBoss;
    public bool isBoss { get { return _isBoss; } set { _isBoss = value; } }

    [SerializeField]
    private bool _isCanSlow = true;
    public bool isCanSlow { get { return _isCanSlow; } set { _isCanSlow = value; } }
    [SerializeField]
    private bool _isCanElectric = true;
    public bool isCanElectric { get { return _isCanElectric; } set { _isCanElectric = value; } }
    [SerializeField]
    private bool _isCanPoison = true;
    public bool isCanPoison { get { return _isCanPoison; } set { _isCanPoison = value; } }
    [SerializeField]
    private bool _isCanRage = true;
    public bool isCanRage { get { return _isCanRage; } set { _isCanRage = value; } }
    [SerializeField]
    private bool _isCanWeakness = true;
    public bool isCanWeakness { get { return _isCanWeakness; } set { _isCanWeakness = value; } }

    public bool isCanDebuff { get { return (_isCanSlow || _isCanElectric || _isCanPoison || _isCanRage || _isCanWeakness); } }

    [SerializeField]
    private bool _isCanParryingStun = true;
    public bool isCanParryingStun { get { return _isCanParryingStun; }  set { _isCanParryingStun = value; } }

    // Start is called before the first frame update
    void Start()
    {
        if (isCanCounterAttacked)
        {
            CanCounterAttackedObject counter = GetComponent<CanCounterAttackedObject>();
            if (!counter)
                counter = gameObject.AddComponent<CanCounterAttackedObject>();

            counter.enemyInfo = this;
        }

        if (isAffectedGravity)
        {
            Movable move = GetComponent<Movable>();
            if (move)
            {
                move.isCanAffectedGravity = isAffectedGravity;
                move.isBoss = isBoss;
            }
        }

        if (isAffectedTimeSlow)
        {
            CanAffectedTimeSlow time = GetComponent<CanAffectedTimeSlow>();
            if (!time)
                time = gameObject.AddComponent<CanAffectedTimeSlow>();

            time.isBoss = isBoss;
        }

        if (isCanDebuff)
        {
            DebuffInfo debuffInfo = GetComponent<DebuffInfo>();
            if (!debuffInfo)
                debuffInfo = gameObject.AddComponent<DebuffInfo>();

            debuffInfo.enemyInfo = this;
        }

        if (isCanParryingStun)
        {
            ParryingStunController stun = GetComponent<ParryingStunController>();
            if (!stun)
                stun = gameObject.AddComponent<ParryingStunController>();

            stun.enemyInfo = this;
        }
    }
}
