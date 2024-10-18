using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiatrisAttacker : BossAttacker
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ListChange()
    {
        for (int i = 0; i < attackSecondList.Count; i++)
        {
            attackList.Add(attackSecondList[i]);
        }
        attackSecondList.Clear();
    }
}
