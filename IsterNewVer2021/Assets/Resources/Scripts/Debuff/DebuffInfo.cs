using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DEBUFF_TYPE
{
    NONE = -1,
    SLOW,
    POISON,
    ELECTRIC,
    WEAKNESS,
    RAGE,
    END
}

public class DebuffInfo : MonoBehaviour
{
    public EnemyInfo enemyInfo { get; set; }

    public float knockbackDragDecrease { get; set; }
    public float damageDecreaseMultiplier { get; set; }
    public float getMoreDamage { get; set; }

    public float slowDecrease { get; set; }
    public float slowMultiplier { get { return (1.0f - slowDecrease); } }

    private List<DebuffBase> _debuffList = new List<DebuffBase>();

    public bool isAbnormal { get { return (_debuffList.Count != 0); } }
    public int abnormalCount { get { return _debuffList.Count; } }

    public float coolTimeMultiplier { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        DebuffReset();

        // test
        DebuffBase debuff;

        //debuff = new DebuffSlow();
        //debuff.totalTime = 500.0f;
        //AddDebuff(debuff);

        //debuff = new DebuffPoison();
        //debuff.totalTime = 500.0f;
        //AddDebuff(debuff);

        //debuff = new DebuffElectric();
        //debuff.totalTime = 500.0f;
        //AddDebuff(debuff);

        //debuff = new DebuffRage();
        //debuff.totalTime = 500.0f;
        //AddDebuff(debuff);

        //debuff = new DebuffWeakness();
        //debuff.totalTime = 500.0f;
        //AddDebuff(debuff);
    }

    void Update()
    {
        for (int i = 0; i < _debuffList.Count;)
        {
            _debuffList[i].Update();

            if (_debuffList[i].isEnd)
                RemoveDebuff(_debuffList[i]);
            else ++i;
        }
    }

    public void AddDebuff(DebuffBase debuff)
    {
        if (enemyInfo)
        {
            if (!enemyInfo.isCanSlow && typeof(DebuffSlow).IsInstanceOfType(debuff)) return;
            if (!enemyInfo.isCanElectric && typeof(DebuffElectric).IsInstanceOfType(debuff)) return;
            if (!enemyInfo.isCanPoison && typeof(DebuffPoison).IsInstanceOfType(debuff)) return;
            if (!enemyInfo.isCanRage && typeof(DebuffRage).IsInstanceOfType(debuff)) return;
            if (!enemyInfo.isCanWeakness && typeof(DebuffWeakness).IsInstanceOfType(debuff)) return;
        }

        bool isAdd = true;
        foreach (DebuffBase de in _debuffList)
        {
            if (de.GetType().IsInstanceOfType(debuff))
            {
                float figure = (de.figure > debuff.figure ? de.figure : debuff.figure);
                de.figure = figure;

                if (!typeof(DebuffElectric).IsInstanceOfType(debuff))
                    de.BuffOn();

                de.AddTotalTime(debuff.totalTime);
                de.TimeReset();

                isAdd = false;

                break;
            }
        }

        if (isAdd)
        {
            debuff.owner = this;
            debuff.Init();
            _debuffList.Add(debuff);
        }

        TracingStackTimer timer = GetComponentInChildren<TracingStackTimer>();
        if (timer)
        {
            Damagable damagable = GetComponent<Damagable>();
            if (damagable)
            {
                Damage realDamage = timer.damage;
                realDamage.damageMultiplier = timer.currentStack;
                damagable.HitDamager(realDamage, Vector2.zero);

                timer.DestroyTimer();
            }
        }
    }

    public void RemoveRandomDebuff()
    {
        if (_debuffList.Count == 0) return;

        int rnd = Random.Range(0, _debuffList.Count);
        RemoveDebuff(_debuffList[rnd]);
    }

    public void RemoveDebuff(DebuffBase debuff)
    {
        debuff.Release();
        _debuffList.Remove(debuff);
    }

    public void RemoveDebuffAll()
    {
        foreach (var debuff in _debuffList)
            debuff.Release();
        _debuffList.Clear();
    }

    public void DebuffTimeUp()
    {
        foreach (var debuff in _debuffList)
            debuff.totalTime += debuff.totalTime * 0.1f;
    }

    void DebuffReset()
    {
        RemoveDebuffAll();

        knockbackDragDecrease = 0.0f;
        damageDecreaseMultiplier = 1.0f;
        getMoreDamage = 0.0f;

        coolTimeMultiplier = 1.0f;

        slowDecrease = 0.0f;
    }

    public void AddSlow(float figure)
    {
        slowDecrease += figure;
    }

    public void RemoveSlow(float figure)
    {
        slowDecrease -= figure;
        slowDecrease = (slowDecrease >= 0.0f ? slowDecrease : 0.0f);
    }
}
