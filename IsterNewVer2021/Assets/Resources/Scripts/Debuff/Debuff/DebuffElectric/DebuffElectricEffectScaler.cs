using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffElectricEffectScaler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!transform.parent) return;

        Collider2D parent = transform.parent.GetComponent<Collider2D>();
        float scaleFactor = parent.bounds.size.x * 2.0f;

        transform.localScale = new Vector3(scaleFactor, scaleFactor, 1.0f);
    }
}
