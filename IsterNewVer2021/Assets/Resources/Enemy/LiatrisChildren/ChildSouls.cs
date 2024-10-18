using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildSouls : EnemyBulletController
{
    [SerializeField]
    Vector3 _targetPos;

    [SerializeField]
    MotherSpirit _motherSpirit;

    //유도 감도 (회전 속도) 속도가 빠르면 유도 각이 가빠르니 
    public float _turnSpeed;

    [SerializeField]
    ParticleSystem _effect;

    //[Range(-180.0f, 180.0f)]
    //[SerializeField]
    //private float viewRotateZ;

    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float _tracingDelay;
    private float _isTracingOnTime;

    [SerializeField]
    private float _durationTime;

    public override void Start()
    {
        base.Start();
       _motherSpirit = FindObjectOfType<MotherSpirit>();
        
        if(_motherSpirit)
        {
            _targetPos = _motherSpirit.transform.position;
            _collider.enabled = true;
            _collider.isTrigger = true;
        }
        else
        {
            _targetPos = new Vector3(transform.position.x, 30);
            _isTracingOnTime = 0;
            Invoke("DestroyBullet", _durationTime);
        }
        
    }

    protected override void ComputeVelocity()
    {
        _isTracingOnTime += IsterTimeManager.enemyDeltaTime;
        if (_motherSpirit)
        {
            if (_isTracingOnTime > _tracingDelay)
            {
                //타겟 방향
                var targetDir = (_targetPos - transform.position).normalized;
                //타겟 방향으로 direction을 부드럽게 조정
                dir = Vector2.Lerp(dir, targetDir, IsterTimeManager.enemyDeltaTime * _turnSpeed);

                var newPos = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
                Quaternion target = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, newPos + 90);
                transform.rotation = Quaternion.Lerp(transform.rotation, target, IsterTimeManager.enemyDeltaTime * _turnSpeed);
            }
            _targetVelocity = dir * _speed * 1.5f;
        }
        else
        {
            if (_isTracingOnTime > _tracingDelay)
            {
                //타겟 방향
                var targetDir = (_targetPos - transform.position).normalized;
                //타겟 방향으로 direction을 부드럽게 조정
                dir = Vector2.Lerp(dir, targetDir, IsterTimeManager.enemyDeltaTime * _turnSpeed);

                var newPos = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
                Quaternion target = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, newPos + 90);
                transform.rotation = Quaternion.Lerp(transform.rotation, target, IsterTimeManager.enemyDeltaTime * _turnSpeed);
            }
            _targetVelocity = dir * _speed * 1.5f;
        }
    }
   // private void OnCollisionEnter2D(Collision2D collision)
   // {
   //     Debug.Log(collision.gameObject.name);
   //     if (_motherSpirit)
   //     {
   //         if (collision.gameObject.name == "Liatris")
   //         {
   //             _effect.Play();
   //             Debug.Log("cresh!!!!");
   //             DestroyBullet();
   //         }
   //     }
   // }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_motherSpirit)
        {
            if (collision.gameObject.name == "Liatris")
            {
                Instantiate(_effect);
                DestroyBullet();
            }
        }
    }
    
    //방향각 구하기
    public float GetAngleATan(Vector3 vStart, Vector3 vTarget)
    {
        Vector3 v = vTarget - vStart;
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }
    //이게 존나 중요한거였음 축 잡아주기
    //입력한 값을 up vector 기준으로 local direction 변화시켜줌
    private Vector3 AngleToDirZ(float angleInDegree)
    {
        float radian = (angleInDegree - transform.eulerAngles.z) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), Mathf.Cos(radian), 0f);
    }
    public override void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
