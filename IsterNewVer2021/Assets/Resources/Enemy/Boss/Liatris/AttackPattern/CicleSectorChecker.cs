using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CicleSectorChecker : MonoBehaviour
{
    CircleSectorShootPattern _circleSector;

    [SerializeField]
    private SpriteRenderer _renderer;
    private Material _mat;

    private void OnEnable()
    {
        _circleSector = FindObjectOfType<CircleSectorShootPattern>();

        _circleSector._right.transform.localRotation = Quaternion.Euler(0, 0, _circleSector._angle / 2);
        _circleSector._left.transform.localRotation = Quaternion.Euler(0, 0, -_circleSector._angle / 2);

        StartCoroutine(Warning());
    }
    private void Update()
    {
        _circleSector._dir = _circleSector._owner.player.transform.position - transform.position;
        //transform.rotation = Quaternion.Slerp(transform.rotation, _circleSector._owner.player.transform.rotation , IsterTimeManager.enemyDeltaTime);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (collision)

        //_circleSector._dotValue = Mathf.Cos(Mathf.Deg2Rad * (_circleSector._angle / 2));

        //if (Vector3.Dot(_circleSector._dir.normalized, transform.forward) > _circleSector._dotValue)
        //    _circleSector._isInside = true;
        //else
        //    _circleSector._isInside = false;

        Vector2 toPlayerDir = CommonFuncs.CalcDir(transform, _circleSector._owner.player);
        float rotAngle = _circleSector._colliderObject.transform.localEulerAngles.z;

        if (rotAngle >= 90.0f)
            rotAngle -= 360.0f;

        float min = rotAngle - _circleSector._angle / 2;
        float max = rotAngle + _circleSector._angle / 2;

        float playerDirAngle = CommonFuncs.DirToDegree(toPlayerDir) - 270.0f;
        
        if (playerDirAngle >= 90.0f)
            playerDirAngle -= 360.0f;

        _circleSector._isInside = (min <= playerDirAngle && playerDirAngle <= max);
           
    }

    IEnumerator Warning()
    {
        if (!_mat)
            _mat = Instantiate<Material>(Resources.Load<Material>("Enemy/Boss/Liatris/PatternBullet/CircleSector/LiatricCircleSectorMat"));

        _renderer.material = _mat;

        ApplyAlpha(0.5f);

        yield return new WaitForEndOfFrame();

        float currentTime = 0.0f;
        float totalTime = _circleSector._insideTime + _circleSector._attackDelay * 2.0f;
        Vector2 startUV = new Vector2(0.0f, -0.2f);
        Vector2 endUV = new Vector2(0.0f, 0.6f);
        Vector2 currentUV = startUV;
        while (currentTime < totalTime)
        {
            float ratio = currentTime / totalTime;

            currentUV = Vector2.Lerp(startUV, endUV, ratio);
            _mat.SetVector("_UvOffset", currentUV);

            yield return null;

            currentTime += IsterTimeManager.bossDeltaTime;
        }
        currentUV = endUV;
        _mat.SetVector("_UvOffset", currentUV);

        yield return null;
    }

    void ApplyAlpha(float alpha)
    {
        Color color = _renderer.color;
        color.a = alpha;
        _renderer.color = color;
    }
}
