using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaloDialogueCondition : DialogueNodeChangerCondition
{
    public override bool IsCanChange()
    {
        int count = PlayerPrefs.GetInt("DaloFirstMeet", -1);

        return (count >= 100);
    }

    public void DaloDialogueEnd()
    {
        PlayerPrefs.SetInt("DaloFirstMeet", 100);
    }
}
