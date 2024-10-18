using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootholdBase : MonoBehaviour
{
    [SerializeField]
    protected bool _isCanActive;
    public bool isCanActive { get { return _isCanActive; } set { _isCanActive = value; } }

    [SerializeField]
    protected Collider2D _collider;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _isCanActive = true;

        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;       
    }

    // Update is called once per frame
    protected virtual void Update()
    { }
    
}
