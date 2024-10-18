using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBomb : MonoBehaviour
{
    public float totalTime { get; set; }
    public float figure { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBase enemy = collision.GetComponent<EnemyBase>();
        if (enemy)
        {
            DebuffSlow slow = new DebuffSlow();
            slow.totalTime = totalTime;

            DebuffInfo debuffInfo = enemy.GetComponent<DebuffInfo>();
            debuffInfo.AddDebuff(slow);
        }
    }
}
