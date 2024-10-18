using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SAURON_DIR
{
    NONE = -1,
    RIGHT,
    UP,
    LEFT,
    DOWN,
    END
}

public class SauronAttackRange : MonoBehaviour
{
    [SerializeField]
    private Transform _range;
    [SerializeField]
    private Transform _laser;

    public bool _isDistance;

    [SerializeField]
    private SAURON_DIR _dir;
    public SAURON_DIR dir { get { return _dir; } set { _dir = value; } }

    [SerializeField]
    public GameObject _attackLine;

    [SerializeField]
    private LayerMask _rayLayer;

    // Start is called before the first frame update
    void Start()
    {
        float angle = 90.0f * (int)_dir;
        _range.rotation = Quaternion.identity;
        _range.Rotate(new Vector3(0.0f, 0.0f, angle));

        _laser.rotation = Quaternion.identity;
        _laser.Rotate(new Vector3(-angle, 90.0001f, 0.0f));

        float scaleFactor = CalcDistance(angle);
        Vector3 scale = _range.localScale;
        scale.x = scaleFactor;
        _range.localScale = scale;
    }

    public float CalcDistance(float dirAngle)
    {
        float distance = 0.0f;
        float dirX = Mathf.Sin(dirAngle * Mathf.Deg2Rad);
        float dirY = Mathf.Cos(dirAngle * Mathf.Deg2Rad);

        Vector2 dir = Vector2.zero;
        switch (_dir)
        {
            case SAURON_DIR.RIGHT:
                dir = Vector2.right;
            break;

            case SAURON_DIR.UP:
                dir = Vector2.up;
            break;

            case SAURON_DIR.LEFT:
                dir = Vector2.left;
                break;

            case SAURON_DIR.DOWN:
                dir = Vector2.down;
            break;
        }

        RaycastHit2D hit;
        hit = Physics2D.Raycast(_range.transform.position, dir, float.PositiveInfinity, _rayLayer);
        if (hit)
        {
            distance = hit.distance; _isDistance = true;
        }

        return distance;
    }
}
