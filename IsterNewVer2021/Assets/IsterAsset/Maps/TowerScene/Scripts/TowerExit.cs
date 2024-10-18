using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerExit : MonoBehaviour
{
    public void ExitTower()
    {
        TowerManager manager = FindObjectOfType<TowerManager>();
        if (manager)
            BlackMaskController.instance.AddEvent(manager.ExitTower, BlackMaskEventType.MIDDLE);
    }
}
