using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLobbyAmbient : MonoBehaviour
{
    private Transform _prevParent;

    [SerializeField]
    private ParticleSystem _effect;

    // Start is called before the first frame update
    void Start()
    {
        _prevParent = transform.parent;
        transform.parent = Camera.main.transform;
        Vector3 newPos = Vector3.zero;
        newPos.z = -Camera.main.transform.position.z;
        transform.localPosition = newPos;
    }

    public void ReturnParent()
    {
        //transform.parent = _prevParent;
        _effect.Stop();
    }
}
