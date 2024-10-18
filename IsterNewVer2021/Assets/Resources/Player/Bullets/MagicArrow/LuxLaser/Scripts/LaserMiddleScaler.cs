using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMiddleScaler : MonoBehaviour
{
    [SerializeField]
    private GameObject _tail;

    [SerializeField]
    private LayerMask _obstacles;

    [SerializeField]
    private float _distance = 1.0f;

    public Vector2 dir { get { return transform.right; } }

    [SerializeField]
    private bool _isContinuous;
    private bool _isEnd;

    [SerializeField]
    private Transform _root;

    void Start()
    {
        _isEnd = false;

        Update();
    }

    void Update()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (_isEnd) return;

        float minDistance = 100.0f;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir, minDistance, _obstacles);
        foreach (RaycastHit2D hit in hits)
        {
            if (!hit) continue;

            Collider2D obstacle = hit.collider;
            if (obstacle.isTrigger) continue;

            if (hit.distance < minDistance)
                minDistance = hit.distance;
        }

        if (_tail)
        {
            Vector3 tailPosition = transform.position + (Vector3)dir * minDistance;
            tailPosition.z = 1.0f;
            TailReposition(tailPosition);
        }

        Vector3 scale = transform.localScale;
        scale.x = minDistance;

        if (_root)
            scale.x /= _root.localScale.x;

        transform.localScale = scale;

        _isEnd = !_isContinuous;
    }

    void TailReposition(Vector2 position)
    {
        _tail.transform.position = position;
    }
}
