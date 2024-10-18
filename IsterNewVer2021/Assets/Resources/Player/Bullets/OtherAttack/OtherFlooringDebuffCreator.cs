using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherFlooringDebuffCreator : MonoBehaviour
{
    public PassiveOtherAttackUser user { get; set; }

    void Start()
    {
        EventTimer timer = GetComponentInParent<EventTimer>();
        timer.AddEvent(Destroy);
    }

    public void Destroy()
    {
        Destroy(transform.parent.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DebuffInfo debuffInfo = collision.GetComponent<DebuffInfo>();
        if (!debuffInfo) return;

        DebuffBase debuff;

        debuff = new DebuffSlow();
        debuff.totalTime = 3.0f;
        debuff.figure = 0.5f + user.level * 0.1f;

        debuffInfo.AddDebuff(debuff);

        if (user.isPoisonFlooring)
        {
            debuff = new DebuffPoison();
            debuff.totalTime = 5.0f;
            debuff.figure = 1 + user.level * 1;

            debuffInfo.AddDebuff(debuff);
        }
    }
}
