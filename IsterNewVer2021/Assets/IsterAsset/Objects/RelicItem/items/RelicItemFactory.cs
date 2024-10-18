using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicItemFactory
{
    public static RelicItemBase CreateItem(int id)
    {
        RelicItemBase newItem = null;

             if (id ==  10) newItem = new TutorialPrisonDoorKey();
        else if (id == 100) newItem = new RelicLight();
        else if (id == 101) newItem = new DaloRoomKey();
        else if (id == 102) newItem = new RelicDashItem();
        else if (id == 103) newItem = new FieldLiatrisSoulStone.Part0();
        else if (id == 104) newItem = new FieldLiatrisSoulStone.Part1();
        else if (id == 105) newItem = new FieldLiatrisSoulStone.Part2();
        else if (id == 106) newItem = new FieldLiatrisSoulStone();

        return newItem;
    }
}
