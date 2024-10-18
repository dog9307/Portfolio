using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFlyingObject : MonoBehaviour
{
    public float _speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, _speed * IsterTimeManager.deltaTime));
    }
}
