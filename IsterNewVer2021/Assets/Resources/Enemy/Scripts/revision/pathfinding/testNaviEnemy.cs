using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testNaviEnemy : REnemyBase
{
    // Start is called before the first frame update
    public override void Init()
    {
        SetEnemy(this);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
