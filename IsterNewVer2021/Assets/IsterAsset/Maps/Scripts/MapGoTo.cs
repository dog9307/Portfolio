using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGoTo : MonoBehaviour
{
    [SerializeField]
    private string _goToMapName;
    //[SerializeField]
    //private int _goToStartPosID;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMapChanger mapChanger = FindObjectOfType<PlayerMapChanger>();
        if (mapChanger)
            mapChanger.ChangeMap(_goToMapName);
    }
}
