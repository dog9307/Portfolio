using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaybeTrash : MonoBehaviour
{
    [SerializeField]
    private bool _isNotTrash = false;
    public bool isNotTrash { get { return _isNotTrash; } set { _isNotTrash = value; } }

    // Start is called before the first frame update
    void Start()
    {
        if (!_isNotTrash)
        {
            if (PrefabTrashCan.instance)
                PrefabTrashCan.instance.AddTrash(gameObject);
        }
    }
}
