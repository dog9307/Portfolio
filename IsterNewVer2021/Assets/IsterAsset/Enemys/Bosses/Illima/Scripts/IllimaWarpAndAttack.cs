using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllimaWarpAndAttack : BossMoveBase
{
    [SerializeField]
    List<Transform> _warpPoints = new List<Transform>();

    Coroutine _coroutine;

    [SerializeField]
    Transform _effectPos;
    [SerializeField]
    private ParticleSystem _effector;

    [HideInInspector]
    public GameObject _furthestObject;

    [SerializeField]
    Transform _warpBulletCreateCenter;
    //public GameObject _check;
    [SerializeField]
    int _prevPointNum;

    public override void Start()
    {
        base.Start();
        _prevPointNum = Random.Range(0, 8);
    }
    public override void PatternOn()
    {
        _movable._moveStart = false;
        _coroutine = StartCoroutine(WarpAndAttack());
    }
    public override void PatternOff()
    {
        _owner.movePatternEnd = true;
        StopCoroutine(_coroutine);
    }
    public override void PatternEnd()
    {
        base.PatternEnd();

        if (_sfx)
            _sfx.PlaySFX(_patternEndSfx);
    }
    IEnumerator WarpAndAttack()
    {      
      
        yield return null;

        PatternOff();
    }
    public void CheckFurthestPoint()
    {
        _furthestObject = null;
        float furthestDistance = 0;

        if (_warpPoints.Count != 0)
        {
            for (int i = 0; i < _warpPoints.Count; i++)
            {
                // Get the distance of this hit with the transform
                float currentDistance = CommonFuncs.Distance(transform.position,_warpPoints[i].transform.position);

                // If the distance is greater, store this hit as the new furthest
                if (currentDistance > furthestDistance)
                {
                   furthestDistance = currentDistance;

                    _furthestObject = _warpPoints[i].gameObject;
                    _furthestObject.transform.position = _warpPoints[i].position;
                }
            }
        }

        _owner.gameObject.transform.position = _furthestObject.transform.position;
    }
    public void WarpRanPos()
    {
        int pointNum = Random.Range(0, 8);

        if (pointNum != _prevPointNum)
        {
            _prevPointNum = pointNum;

            _furthestObject = _warpPoints[pointNum].gameObject;
            _furthestObject.transform.position = _warpPoints[pointNum].position;

            _owner.gameObject.transform.position = _furthestObject.transform.position;
        }
        else
        {
            WarpRanPos();
        }
    }
    public void Clap()
    {
        GameObject newBullet = CreateObject();
        newBullet.transform.position = _warpBulletCreateCenter.transform.position;

        if (_effector)
        {
            ParticleSystem effector = Instantiate<ParticleSystem>(_effector);

            effector.transform.position = _effectPos.transform.position;
            effector.Play();
        }

        // CheckFurthestPoint();
        WarpRanPos();
    }
}
