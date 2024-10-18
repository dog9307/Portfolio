using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSectorCircleWarning : MonoBehaviour
{
    FlowerSectorCircle _flowerSectorCircle;

    [SerializeField]
    private SpriteRenderer _renderer;
    private Material _mat;

    public Coroutine _coroutine;
    // Start is called before the first frame update
    private void OnEnable()
    { 
        _flowerSectorCircle = FindObjectOfType<FlowerSectorCircle>();
        _coroutine = StartCoroutine(Warning());
        _flowerSectorCircle._dir = CommonFuncs.CalcDir(transform, _flowerSectorCircle._player.transform);
        float rotAngle = CommonFuncs.DirToDegree(_flowerSectorCircle._dir) - 270.0f;

        if (rotAngle >= 90.0f)
            rotAngle -= 360.0f;

        transform.localRotation = Quaternion.identity;
        transform.Rotate(new Vector3(0.0f, 0.0f, rotAngle));

    } 
    IEnumerator Warning()
    {
        if (!_mat)
            _mat = Instantiate<Material>(Resources.Load<Material>("Enemy/Boss/Liatris/PatternBullet/CircleSector/LiatricCircleSectorMatCenter"));

        _renderer.material = _mat;

        ApplyAlpha(0.5f);

        yield return new WaitForEndOfFrame();

        float currentTime = 0.0f;
        float totalTime = _flowerSectorCircle._fireTime;
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

        _flowerSectorCircle._corutine = _flowerSectorCircle.StartCoroutine(_flowerSectorCircle.ShootTheBullet());

        StopCoroutine(_coroutine);

        //yield return null;
    }

    void ApplyAlpha(float alpha)
    {
        Color color = _renderer.color;
        color.a = alpha;
        _renderer.color = color;
    }
}
