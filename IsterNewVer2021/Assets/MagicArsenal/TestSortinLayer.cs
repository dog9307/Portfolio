using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSortinLayer : MonoBehaviour
{
    [SerializeField]
    private string _layerName = "Effect";

    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
        foreach (var par in particles)
            par.GetComponent<ParticleSystemRenderer>().sortingLayerName = _layerName;
    }
}
