using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointerParticleRepositioner : MonoBehaviour
{
    [SerializeField]
    private Vector2 offset;

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition + (Vector3)offset;
        transform.position = mousePos;
    }
}
