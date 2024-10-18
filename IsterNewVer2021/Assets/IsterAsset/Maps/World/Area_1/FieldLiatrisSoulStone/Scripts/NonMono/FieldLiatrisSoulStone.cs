using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldLiatrisSoulStone : RelicItemBase
{
    public class Part0 : RelicItemBase
    {
        public override void Init()
        {
            id = 103;
        }

        public override void Release()
        {
        }

        public override void UseItem()
        {
            PlayerInventory inventory = GameObject.FindObjectOfType<PlayerInventory>();

            bool isAllGain = true;
            for (int i = 103; i <= 105; ++i)
            {
                RelicItemBase stone = inventory.FindRelicItem(i);
                if (stone == null)
                {
                    isAllGain = false;
                    break;
                }    
            }

            if (isAllGain)
            {
                for (int i = 103; i <= 105; ++i)
                    inventory.RemoveRelicItem(i);

                RelicItemBase newItem = RelicItemFactory.CreateItem(106);
                if (newItem == null)
                    return;

                inventory.AddRelicItem(newItem);

                GameObject dialogue = GameObject.Find("FieldLiatrisStoneAllGain");
                if (dialogue)
                    dialogue.GetComponent<CutSceneController>().StartCutScene();
            }
        }
    }
    public class Part1 : RelicItemBase
    {
        public override void Init()
        {
            id = 104;
        }

        public override void Release()
        {
        }

        public override void UseItem()
        {
            PlayerInventory inventory = GameObject.FindObjectOfType<PlayerInventory>();

            bool isAllGain = true;
            for (int i = 103; i <= 105; ++i)
            {
                RelicItemBase stone = inventory.FindRelicItem(i);
                if (stone == null)
                {
                    isAllGain = false;
                    break;
                }
            }

            if (isAllGain)
            {
                for (int i = 103; i <= 105; ++i)
                    inventory.RemoveRelicItem(i);

                RelicItemBase newItem = RelicItemFactory.CreateItem(106);
                if (newItem == null)
                    return;

                inventory.AddRelicItem(newItem);

                GameObject dialogue = GameObject.Find("FieldLiatrisStoneAllGain");
                if (dialogue)
                    dialogue.GetComponent<CutSceneController>().StartCutScene();
            }
        }
    }

    public class Part2 : RelicItemBase
    {
        public override void Init()
        {
            id = 105;
        }

        public override void Release()
        {
        }

        public override void UseItem()
        {
            PlayerInventory inventory = GameObject.FindObjectOfType<PlayerInventory>();

            bool isAllGain = true;
            for (int i = 103; i <= 105; ++i)
            {
                RelicItemBase stone = inventory.FindRelicItem(i);
                if (stone == null)
                {
                    isAllGain = false;
                    break;
                }
            }

            if (isAllGain)
            {
                for (int i = 103; i <= 105; ++i)
                    inventory.RemoveRelicItem(i);

                RelicItemBase newItem = RelicItemFactory.CreateItem(106);
                if (newItem == null)
                    return;

                inventory.AddRelicItem(newItem);

                GameObject dialogue = GameObject.Find("FieldLiatrisStoneAllGain");
                if (dialogue)
                    dialogue. GetComponent<CutSceneController>().StartCutScene();
            }
        }
    }

    public override void Init()
    {
        id = 106;
    }

    public override void Release()
    {
    }
}
