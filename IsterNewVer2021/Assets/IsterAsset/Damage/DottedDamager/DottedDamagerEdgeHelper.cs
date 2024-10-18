using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class DottedDamagerEdgeHelper : MonoBehaviour
{
    [SerializeField]
    private DottedDamager _damager;

    void Start()
    {
        EdgeCollider2D[] cols = GetComponents<EdgeCollider2D>();
        for (int i = 0; i < cols.Length; ++i)
            cols[i].isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_damager) return;
        if (_damager.owner.ToString().Equals(collision.tag)) return;

        Damagable damagable = collision.GetComponent<Damagable>();
        if (damagable)
            _damager.AddToDictionary(damagable);
    }
}
