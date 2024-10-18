using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyTreeBarrier : MonoBehaviour
{
    [SerializeField]
    private float _decrease;
    public float decrease { get { return _decrease; } set { _decrease = value; } }

    private void OnTriggerStay2D(Collider2D collision)
    {
        BuffInfo player = collision.GetComponent<BuffInfo>();
        if (!player) return;

        player.familiarDamageDecrease = decrease;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        BuffInfo player = collision.GetComponent<BuffInfo>();
        if (!player) return;

        player.familiarDamageDecrease = 0.0f;
    }
}
