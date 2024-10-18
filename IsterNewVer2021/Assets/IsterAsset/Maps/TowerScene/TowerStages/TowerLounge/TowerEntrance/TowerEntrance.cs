using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEntrance : MonoBehaviour
{
    [SerializeField]
    private TowerLobbyAmbient _ambient;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMoveController player = collision.GetComponent<PlayerMoveController>();
        if (player)
        {
            TowerManager manager = FindObjectOfType<TowerManager>();
            if (manager)
                manager.StageClear();

            _ambient.ReturnParent();
        }
    }
}
