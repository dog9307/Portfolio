using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformRepositioner : MonoBehaviour
{
    [SerializeField]
    private Transform _reposition;

    public void Reposition(Transform target)
    {
        target.transform.position = _reposition.position;
    }
}
