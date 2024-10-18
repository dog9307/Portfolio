using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DongleYeeSight : MonoBehaviour
{
    [SerializeField]
    private Transform _lookPoint;

    private NavMeshAgent _agent;
    private Rigidbody2D _rigid;

    public Vector2 dir
    {
        get
        {
            if (!_lookPoint)
                return Vector2.right;

            return CommonFuncs.CalcDir(this, _lookPoint);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_agent)
            _agent = GetComponentInParent<NavMeshAgent>();
        if (!_rigid)
            _rigid = GetComponentInParent<Rigidbody2D>();

        if (!_agent && !_rigid) return;

        Vector2 dir = Vector2.right;

        if (_agent && _agent.enabled)
        {
            if (_agent.velocity.magnitude < float.Epsilon) return;

            dir = _agent.velocity.normalized;

            float angle = 0.0f;
            if (dir.magnitude != 0.0f)
                angle = CommonFuncs.DirToDegree(dir);

            transform.localRotation = Quaternion.identity;
            transform.Rotate(new Vector3(0.0f, 0.0f, angle));
        }
    }
}
