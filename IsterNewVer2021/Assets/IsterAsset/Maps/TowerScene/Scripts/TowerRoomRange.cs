using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TowerRoomRange : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _relativeVCam;

    private CinemachineConfiner _con;
    private PolygonCollider2D _polygon;
    private float _damping;

    [SerializeField]
    private bool _isXSkip;
    [SerializeField]
    private bool _isYSkip;

    // Start is called before the first frame update
    void Start()
    {
        if (!_relativeVCam.TryGetComponent<CinemachineConfiner>(out _con))
        {
            Destroy(this);
            return;
        }

        _polygon = _con.m_BoundingShape2D as PolygonCollider2D;
        _damping = _con.m_Damping;
        
        ResizeRange();
    }

    void ResizeRange()
    {
        Vector2[] path = _polygon.GetPath(0);

        bool isMinus = false;
        float origin = 0.0f;
        float newValue = 0.0f;
        for (int i = 0; i < path.Length; ++i)
        {
            if (!_isXSkip)
            {
                origin = path[i].x;
                isMinus = (origin < 0.0f);

                newValue = Mathf.Abs(origin) - _damping * _relativeVCam.m_Lens.OrthographicSize;
                path[i].x = (isMinus ? -newValue : newValue);
            }

            if (!_isYSkip)
            {
                origin = path[i].y;
                isMinus = (origin < 0.0f);

                newValue = Mathf.Abs(origin) - _damping * _relativeVCam.m_Lens.OrthographicSize;
                path[i].y = (isMinus ? -newValue : newValue);
            }
        }

        _polygon.SetPath(0, path);
    }
}
