using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiatrisSphereType : MonoBehaviour
{
    [SerializeField]
    private LiatrisController _liatris;

    public FlowerType type
    {
        get
        {
            if (!_liatris)
                return FlowerType.NONE;
            return _liatris.type;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TowerF1Flower flower = collision.GetComponent<TowerF1Flower>();
        if (flower)
            flower.BossGrogiSignal(type);
    }
}
