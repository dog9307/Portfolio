using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracingArrowController : MonoBehaviour
{
    public Collider2D target { get; set; }
    public Vector2 dir { get; set; }

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _brake;

    private float _maxSpeed;

    private Vector3 _targetPos;
    private bool _isTraceOn;
    public bool isTraceOn { get { return _isTraceOn; } set { _isTraceOn = value; } }

    void Start()
    {
        _isTraceOn = false;
        _maxSpeed = _speed;

        Vector2 dir = CommonFuncs.CalcDir(target.bounds.center, transform.position);
        dir.x += Random.Range(-1.0f, 1.0f);
        dir.y += Random.Range(-1.0f, 1.0f);

        this.dir = dir.normalized;

        CalcAngle();
    }

    public void CalcAngle()
    {
        float dot = Vector3.Dot(Vector2.right, dir);
        float angle = Mathf.Acos(dot);

        if (dir.y < 0.0f)
            angle = Mathf.PI * 2 - angle;

        angle = angle * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, angle));
    }

    void Update()
    {
        if (!_isTraceOn)
        {
            _speed -= _brake * IsterTimeManager.enemyTimeScale;
            if (_speed <= 0.0f)
            {
                _speed = _maxSpeed;
                _isTraceOn = true;
            }
        }
        else
        {
            if (target)
            {
                if (target.bounds.size.x == 0.0f ||
                    target.bounds.size.y == 0.0f)
                    Destroy(gameObject);
                else
                    _targetPos = target.bounds.center;
            }
            else
                Destroy(gameObject);

            dir = (_targetPos - transform.position).normalized;

            CalcAngle();
        }

        transform.position += (Vector3)dir * _speed * IsterTimeManager.enemyDeltaTime;
    }
}
