using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingIllustSlotList : MonoBehaviour
{
    [SerializeField]
    private EndingIllustButtonContoller[] _slots;

    void Start()
    {
        foreach (var b in _slots)
        {
            string key = "Ending_" + b.keyward;
            int count = PlayerPrefs.GetInt(key, 0);
            if (count >= 100)
                b.OpenButton();
        }
    }
}
