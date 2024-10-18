using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracingBulletController : EnemyBulletController
{
    PlayerMoveController _tracingTarget;

    //유도 감도 (회전 속도) 속도가 빠르면 유도 각이 가빠르니 
    public float _turnSpeed;

    //[Range(-180.0f, 180.0f)]
    //[SerializeField]
    //private float viewRotateZ;

    [Range(0.0f,2.0f)]
    [SerializeField]
    private float _tracingDelay;
    private float _isTracingOnTime;

    [SerializeField]
    private float _durationTime;

    public override void Start()
    {
        base.Start();
        _isTracingOnTime = 0;
        _tracingTarget = FindObjectOfType<PlayerMoveController>();
        
        Invoke("DestroyBullet", _durationTime);
    }

    protected override void ComputeVelocity()
    {
        _isTracingOnTime += IsterTimeManager.enemyDeltaTime;

        if (_isTracingOnTime > _tracingDelay)
        {  //
           //var bulletangle = GetAngleATan(transform.position, _tracingTarget.transform.position);
           //Quaternion target = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, newPos - 90);
           //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * _turnSpeed);
           //Quaternion target = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, bulletangle);

            //타겟 방향
            var targetDir = (_tracingTarget.center - transform.position).normalized;
            //타겟 방향으로 direction을 부드럽게 조정
            dir = Vector2.Lerp(dir, targetDir, IsterTimeManager.enemyDeltaTime * _turnSpeed);

            var newPos = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
            Quaternion target = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, newPos + 90);
            transform.rotation = Quaternion.Lerp(transform.rotation, target, IsterTimeManager.enemyDeltaTime * _turnSpeed);
        }

        _targetVelocity = dir * _speed;
    }

   // public Vector2 TracingDirection(Vector2 _tracingDelay)
   // {
   //     Vector2 lookDir = AngleToDirZ(viewRotateZ);
   //
   //     float dot = Vector2.Dot(lookDir, dir);
   //     if (dot > 1.0f) dot = 1.0f;
   //     else if (dot < 0.0f) dot = 0.0f;
   //
   //     float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
   //
   //     return dir;
   // }

    //플레이어 인식 범위판별 halfveiwDegree 안에 있을시 true
    //public bool FindViewTarget(Vector2 dir)
    //{
    //    Vector2 lookDir = AngleToDirZ(viewRotateZ);

    //    float dot = Vector2.Dot(lookDir, dir);
    //    if (dot > 1.0f) dot = 1.0f;
    //    else if (dot < 0.0f) dot = 0.0f;

    //    float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

    //    return (angle <= angle);
    //}
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
