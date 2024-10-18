using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTabUI : MonoBehaviour
{
    public void TabOn()
    {
        KeyManager.instance.Enable("tabUI");
        if (UIManager.instance)
            UIManager.instance.tabOn = true;
    }
}
