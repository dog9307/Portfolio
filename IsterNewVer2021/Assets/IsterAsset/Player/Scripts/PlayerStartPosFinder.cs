using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPosFinder : MonoBehaviour
{
    public int nextStartPosID { get; set; }

    public void LoadStartPos()
    {
        nextStartPosID = PlayerPrefs.GetInt("PlayerNextStartPos", -1);
        FindStartPos();
    }

    public void FindStartPos(bool isLoad = false)
    {
        if (isLoad)
            LoadStartPos();

        MapStartPos[] poses = FindObjectsOfType<MapStartPos>();
        foreach (var pos in poses)
        {
            if (pos.id == nextStartPosID)
            {
                pos.PlayerStarting();

                PlayerMapChanger.isFindStartPos = true;

                AmbientController ambient = FindObjectOfType<AmbientController>();
                if (ambient)
                    ambient.AmbientReset();

                break;
            }
        }
    }
}
