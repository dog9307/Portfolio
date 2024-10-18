using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class REnemySightController : MonoBehaviour
{
    [HideInInspector]
    public REnemyBase _owner;
    
    [HideInInspector]
    public REnemyMovable _move;

    [HideInInspector]
    public Movable _targetCenter;
    
    [Range(0.0f, 360.0f)]
    [SerializeField]
    private float viewDegree = 0f;
    [Range(-180.0f, 180.0f)]
    [SerializeField]
    private float viewRotateZ;

    [SerializeField]
    private GameObject _lookPoint;
    public Vector2 currentDir
    {
        get
        {
            if (!_lookPoint) return Vector2.zero;

            return CommonFuncs.CalcDir(this, _lookPoint.transform);
        }
    }

    //플레이어 보는 방향 벡터
    public Vector2 targetDir { get; set; }

    public Vector3 virDir { get; set; }

    private float _turnAngle;
    public float turnAngle { get { return _turnAngle; } set { _turnAngle = value; } }

    public float _turnSpeed;
    //디버그
    public bool _Debug;

    private float halfviewDegree = 0f;

    private float _idleTimer = 5.0f;
    public float _currentTimer = 5.0f;

    //가상 좌표용
    double posX = 0;
    double posY = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _move = GetComponentInParent<REnemyMovable>();
        halfviewDegree = viewDegree * 0.5f;
    }
    // Update is called once per frame
    void Update()
    {
        if (_owner.Target)
        {
            _targetCenter = _owner.Target.GetComponent<Movable>();
            var newPos = 0.0f;
            //타겟을 따라가는가
            if (!_owner._controller.attacker.isActiveAndEnabled)
            {
                if (_move._moveType == MP.tracking)
                {
                    if (_owner._agent)
                    {
                        targetDir = _owner._agent.velocity.normalized;
                        newPos = GetAngleATan(transform.position, _owner._agent.steeringTarget);
                    }
                    else
                    {
                        targetDir = (_targetCenter.center - transform.position).normalized;
                        newPos = GetAngleATan(transform.position, _targetCenter.center);
                    }
                    // targetDir = (_targetCenter.center - transform.position).normalized;
                    // newPos = GetAngleATan(transform.position, _targetCenter.center);
                    Quaternion target = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, newPos - 90);
                    transform.rotation = Quaternion.Slerp(transform.rotation, target, IsterTimeManager.enemyDeltaTime * _turnSpeed);

                    _owner._intoSight = FindViewTarget(targetDir);
                }//
                else if (_move._moveType == MP.dash)
                {
                    if (!_owner._controller.movable._isDash)
                    {
                        if (_owner._agent)
                        {
                            targetDir = _owner._agent.velocity.normalized;

                            newPos = GetAngleATan(transform.position, _owner._agent.steeringTarget);
                        }
                        else
                        {
                            targetDir = (_targetCenter.center - transform.position).normalized;
                            newPos = GetAngleATan(transform.position, _targetCenter.center);
                        }

                        virDir = targetDir;

                        Quaternion target = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, newPos - 90);
                        transform.rotation = Quaternion.Slerp(transform.rotation, target, IsterTimeManager.enemyDeltaTime * _turnSpeed);

                        _owner._intoSight = FindViewTarget(targetDir);
                   }
                    //else
                    //{
                    //    targetDir = virDir;
                    //}
                }//아닌가
                else
                {
                    _currentTimer += IsterTimeManager.enemyDeltaTime;

                    if (_currentTimer > _idleTimer)
                    {
                        _currentTimer = 0;

                        posX = Random.Range(-1.0f, 1.0f);
                        posY = Random.Range(-1.0f, 1.0f);

                        posX = System.Math.Truncate(posX * 10) / 10;
                        posY = System.Math.Truncate(posY * 10) / 10;

                        virDir = new Vector3(this.transform.position.x + (float)(posX * 100), this.transform.position.y + (float)(posY * 100));
                    }

                    targetDir = (virDir - this.transform.position).normalized;

                    newPos = GetAngleATan(transform.position, virDir);
                    Quaternion target = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, newPos - 90);
                    transform.rotation = Quaternion.Slerp(transform.rotation, target, IsterTimeManager.enemyDeltaTime * _turnSpeed);

                    _owner._intoSight = FindViewTarget(targetDir);
                }
            }
        }//타겟이 없을 때
        else
        {
            _currentTimer += IsterTimeManager.enemyDeltaTime;

            if (_currentTimer > _idleTimer)
            {
                _currentTimer = 0;

                posX = Random.Range(-1.0f, 1.0f);
                posY = Random.Range(-1.0f, 1.0f);

                posX = System.Math.Truncate(posX * 10) / 10;
                posY = System.Math.Truncate(posY * 10) / 10;

                virDir = new Vector3(this.transform.position.x + (float)(posX * 100), this.transform.position.y + (float)(posY * 100));
            }

            targetDir = (virDir - this.transform.position).normalized;

            var newPos = GetAngleATan(transform.position, virDir);
            Quaternion target = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, newPos - 90);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, IsterTimeManager.enemyDeltaTime * _turnSpeed);

            _owner._intoSight = FindViewTarget(targetDir);
        }    
    }
    //시야각 앞에 장애물이 있는지 팔별하는 함수(길찾기 야매버전)
    //public bool FindObs()
    //{
    //    int playerLayerMask;
    //    int playerBulletLayerMask;
    //    int playerMirageLayerMask;
    //    RaycastHit hit;

    //    playerLayerMask = LayerMask.GetMask("Player");
    //    playerBulletLayerMask = LayerMask.GetMask("PlayerBullet");
    //    playerMirageLayerMask = LayerMask.GetMask("PlayerMirage");
    //    if (!Physics.Raycast(transform.position, currentDir, out hit, _owner._controller._moveRange, playerLayerMask) || 
    //        !Physics.Raycast(transform.position, currentDir, out hit, _owner._controller._moveRange, playerBulletLayerMask) ||
    //        !Physics.Raycast(transform.position, currentDir, out hit, _owner._controller._moveRange, playerMirageLayerMask))
    //    {
    //        _isObs = true;
    //    }
    //    else _isObs = false;


    //    return _isObs;
    //}

    /*---------------------------------------------------------------------------------------
    //따라가는지 패턴.
    //public void MoveDirSetter(bool tracying)
    //{
    //    //타겟이 있을 때.
    //    if (_owner.Target)
    //    {
    //        //타겟을 따라가는가
    //        if (!_owner._controller.attacker.isActiveAndEnabled)
    //        {
    //            if (tracying)
    //            {
    //                targetDir = (_targetCenter.center - transform.position).normalized;
    //                var newPos = GetAngleATan(transform.position, _targetCenter.center);

    //                Quaternion target = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, newPos - 90);
    //                transform.rotation = Quaternion.Slerp(transform.rotation, target, IsterTimeManager.enemyDeltaTime * _turnSpeed);
    //            }//아닌가
    //            else
    //            {
    //                _currentTimer += IsterTimeManager.enemyDeltaTime;

    //                if (_currentTimer > _idleTimer)
    //                {
    //                    _currentTimer = 0;

    //                    posX = Random.Range(-1.0f, 1.0f);
    //                    posY = Random.Range(-1.0f, 1.0f);

    //                    posX = System.Math.Truncate(posX * 10) / 10;
    //                    posY = System.Math.Truncate(posY * 10) / 10;

    //                    virDir = new Vector3(this.transform.position.x + (float)(posX * 100), this.transform.position.y + (float)(posY * 100));
    //                }

    //                targetDir = (virDir - this.transform.position).normalized;

    //                var newPos = GetAngleATan(transform.position, virDir);
    //                Quaternion target = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, newPos - 90);
    //                transform.rotation = Quaternion.Slerp(transform.rotation, target, IsterTimeManager.enemyDeltaTime * _turnSpeed);
    //            }
    //        }
    //    }//타겟이 없을 때
    //    else
    //    {
    //        _currentTimer += IsterTimeManager.enemyDeltaTime;

    //        if (_currentTimer > _idleTimer)
    //        {
    //            _currentTimer = 0;

    //            posX = Random.Range(-1.0f, 1.0f);
    //            posY = Random.Range(-1.0f, 1.0f);

    //            posX = System.Math.Truncate(posX * 10) / 10;
    //            posY = System.Math.Truncate(posY * 10) / 10;

    //            virDir = new Vector3(this.transform.position.x + (float)(posX * 100), this.transform.position.y + (float)(posY * 100));
    //        }

    //        targetDir = (virDir - this.transform.position).normalized;

    //        var newPos = GetAngleATan(transform.position, virDir);
    //        Quaternion target = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, newPos - 90);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, target, IsterTimeManager.enemyDeltaTime * _turnSpeed);

    //        _owner._intoSight = FindViewTarget(targetDir);
    //    }
    }*/

    //플레이어 인식 범위판별 halfveiwDegree 안에 있을시 true
    public bool FindViewTarget(Vector3 dir)
    {
        Vector3 lookDir = AngleToDirZ(viewRotateZ);

        float dot = Vector3.Dot(lookDir, dir);
        if (dot > 1.0f) dot = 1.0f;
        else if (dot < 0.0f) dot = 0.0f;

        turnAngle = Mathf.Acos(dot) * Mathf.Rad2Deg;

        return (turnAngle <= halfviewDegree);
    }
    //방향각 구하기
    public float GetAngleATan(Vector3 vStart, Vector3 vTarget)
    {
        Vector3 v = vTarget - vStart;
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }
    //방향각 반대로 구하기
    public float RGetAngleATan(Vector3 vStart, Vector3 vTarget)
    {
        Vector3 v = vStart - vTarget;
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }
    //이게 존나 중요한거였음 축 잡아주기
    //입력한 값을 up vector 기준으로 local direction 변화시켜줌
    private Vector3 AngleToDirZ(float angleInDegree)
    {
        float radian = (angleInDegree - transform.eulerAngles.z) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), Mathf.Cos(radian), 0f);
    }

    public Vector2 ChangeAngleToDirection(float angle)
    {
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    private void OnDrawGizmos()
    {
        if (_Debug && _owner)
        {
            halfviewDegree = viewDegree * 0.5f;

            Vector3 originpos = transform.position;
            //Gizmos.DrawSphere(originpos, find.checkingRadius);

            Vector3 horizontalRightDir = AngleToDirZ(-halfviewDegree + viewRotateZ);
            Vector3 horizontalLeftDir = AngleToDirZ(halfviewDegree + viewRotateZ);
            Vector3 lookDir = AngleToDirZ(viewRotateZ);

            Debug.DrawRay(originpos, horizontalLeftDir * 3, Color.cyan);
            Debug.DrawRay(originpos, lookDir * 3, Color.green);
            Debug.DrawRay(originpos, horizontalRightDir * 3, Color.cyan);
        }
    }
}
