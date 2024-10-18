using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marking : MonoBehaviour
{
    [SerializeField]
    private GameObject _marker;

    public ActiveMarkerUser user { get; set; }

    void Start()
    {
        transform.localScale = new Vector3(user.scaleFactor, user.scaleFactor, 1.0f);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damagable = collision.GetComponent<Damagable>();
        if (!damagable) return;

        DamageMarker marker = collision.GetComponentInChildren<DamageMarker>();
        if (!marker)
        {
            GameObject newMarker = Instantiate(_marker);
            newMarker.transform.parent = collision.transform;

            marker = newMarker.GetComponent<DamageMarker>();
            marker.activeUser = user;
            marker.currentStack = 1;
        }
        else
        {
            if (user.isStack)
                ++marker.currentStack;
        }

        DebuffInfo debuffInfo = collision.GetComponent<DebuffInfo>();
        if (debuffInfo)
            user.RandomDebuff(debuffInfo);

        damagable.marker = marker;
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    DamageMarker marker = collision.GetComponentInChildren<DamageMarker>();
    //    if (marker)
    //        Destroy(marker.gameObject);
    //}
}
