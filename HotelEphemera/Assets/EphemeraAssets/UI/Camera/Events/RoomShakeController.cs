using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomShakeController : MonoBehaviour
{
    //흔들 카메라 내 오브젝트
    public List<Transform> shakeObjects = new List<Transform>();
    public bool _shakeRotate = false;

    //초기 위치와 각도
    private List<Vector3> prevPos = new List<Vector3>();
    private List<Quaternion> prevRot = new List<Quaternion>();

    //쉐이크 시간
    [Range(0.0f, 100.0f)]
    public float _shakeTime;
    //쉐이크 움직임
    [Range(0.0f, 100.0f)]
    public float _shakePos;
    //쉐이크 각도 (지금은 안쓸듯)
    [Range(0.0f, 100.0f)]
    public float _shakeRot;
    // Start is called before the first frame update
    void Start()
    {
        if (shakeObjects.Count > 0)
        {
            for (int i = 0; i < shakeObjects.Count - 1; i++)
            {
                prevPos.Add(shakeObjects[i].localPosition);
                prevRot.Add(shakeObjects[i].localRotation);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            StartCoroutine(CameraShake(_shakeTime, _shakePos, _shakeRot));
        }
    }
    public IEnumerator CameraShake(float _shakeTime, float _shakePos, float _shakeRot) //시간, 위치, 각도
    {
        float passTime = 0.0f;
        while (passTime < _shakeTime)
        {
            for (int i = 0; i < shakeObjects.Count - 1; i++)
            {
                Vector3 shakePos = Random.insideUnitSphere;
                shakeObjects[i].localPosition = shakePos * _shakePos;

                if (_shakeRotate)
                {
                    //PerlinNoise = 난수 함수  z축 기준으로 움직여야 하기 때문에 z축 기준.
                    Vector3 shakeRot = new Vector3(0, 0, Mathf.PerlinNoise(Time.time * _shakeRot, 0.0f));
                    shakeObjects[i].localRotation = Quaternion.Euler(shakeRot);
                }
                passTime += Time.deltaTime;
            }
            yield return null;
        }

        for (int i = 0; i < shakeObjects.Count - 1; i++)
        {
            shakeObjects[i].localPosition = prevPos[i];
            shakeObjects[i].localRotation = prevRot[i];
        }
    }

    public IEnumerator LookAround()
    {

        yield return null;
    }
}
