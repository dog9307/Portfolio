using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracingArrowDamager : Damager, IObjectCreator
{
    private TracingArrowController _controller;

    public TracingArrowUser user { get; set; }

    [SerializeField]
    private GameObject _stackTimer;
    public GameObject effectPrefab { get { return _stackTimer; } set { _stackTimer = value; } }

    public bool isDebuffTimeUp { get; set; }

    void Start()
    {
        _controller = GetComponent<TracingArrowController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!this._controller.isTraceOn) return;

        Damagable damagable = collision.GetComponent<Damagable>();
        _controller = GetComponent<TracingArrowController>();
        if (_controller && damagable)
        {
            if (!_controller.enabled) return;
            if (_controller.target == collision)
            {
                Vector2 dir = CommonFuncs.CalcDir(this, collision);
                damagable.HitDamager(_damage, dir);

                _controller.enabled = false;

                Animator anim = GetComponent<Animator>();
                if (anim)
                    anim.SetTrigger("effectStart");

                if (!user) return;

                DebuffInfo debuffInfo = _controller.target.GetComponent<DebuffInfo>();
                if (debuffInfo)
                    user.RandomDebuff(debuffInfo);

                if (user.isStackTracing)
                {
                    TracingStackTimer timer = _controller.target.GetComponentInChildren<TracingStackTimer>();
                    if (timer)
                        ++timer.currentStack;
                    else
                        CreateTimer(_controller.target.gameObject);
                }

                if (user.isDebuffTimeUp)
                    DebuffTimeUp(debuffInfo);
            }
        }
    }

    public void CreateTimer(GameObject target)
    {
        GameObject newTimer = CreateObject();
        newTimer.transform.parent = target.transform;
        newTimer.transform.localPosition = Vector2.zero;

        TracingStackTimer timer = newTimer.GetComponent<TracingStackTimer>();
        timer.user = user;
    }

    public GameObject CreateObject()
    {
        GameObject newTimer = Instantiate(_stackTimer);

        return newTimer;
    }

    public void DebuffTimeUp(DebuffInfo debuffInfo)
    {
        if (!debuffInfo) return;

        debuffInfo.DebuffTimeUp();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnTriggerEnter2D(collision);
    }
}
