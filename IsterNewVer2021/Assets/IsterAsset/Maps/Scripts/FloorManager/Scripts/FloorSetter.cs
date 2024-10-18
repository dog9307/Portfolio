using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSetter : MonoBehaviour
{
    [SerializeField]
    private FloorManager manager;

    [SerializeField]
    private int _startFloor;

    // Start is called before the first frame update
    void Start()
    {
        if (!manager)
            manager = FindObjectOfType<FloorManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMoveController player = collision.GetComponent<PlayerMoveController>();
        if (!player) return;

        manager.SetFloor(_startFloor);

        //manager.SetBlur();
        manager.SettersOff();
    }
}
