using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRuleItemFactory
{
    public static TowerRuleItemBase CreateItem(int id)
    {
        TowerRuleItemBase newItem = null;

             if (id == 100) newItem = new TowerF1AdditionalHP();
        else if (id == 101) newItem = new TowerF1SpeedBuff();
        else if (id == 102) newItem = new TowerF1LowVisible();
        else if (id == 103) newItem = new TowerF1Ggwang();
        else if (id == 104) newItem = new TowerGardenFinalKey();
        else if (id == 105) ;

        return newItem;
    }
}
