using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingTransformUI : MonoBehaviour
{
    [SerializeField]
    private Transform _follow;

    [SerializeField]
    private Vector2 _offset;
    [SerializeField]
    private bool _isFollowWithStart = true;

    public bool isFollow { get; set; }

    private Transform _originParent;

    // Start is called before the first frame update
    void Start()
    {
        _originParent = transform.parent;

        if (_isFollowWithStart)
            FollowStart();
    }

    public void FollowStart()
    {
        if (isFollow) return;

        isFollow = true;

        transform.parent = _follow.transform;
        transform.localPosition = _offset;
    }

    public void FollowEnd()
    {
        if (!isFollow) return;

        isFollow = false;

        transform.parent = _originParent;
        transform.position = _follow.transform.position + (Vector3)_offset;
    }
}
