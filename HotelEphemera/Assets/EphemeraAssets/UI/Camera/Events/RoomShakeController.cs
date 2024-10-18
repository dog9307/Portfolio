using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomShakeController : MonoBehaviour
{
    //��� ī�޶� �� ������Ʈ
    public List<Transform> shakeObjects = new List<Transform>();
    public bool _shakeRotate = false;

    //�ʱ� ��ġ�� ����
    private List<Vector3> prevPos = new List<Vector3>();
    private List<Quaternion> prevRot = new List<Quaternion>();

    //����ũ �ð�
    [Range(0.0f, 100.0f)]
    public float _shakeTime;
    //����ũ ������
    [Range(0.0f, 100.0f)]
    public float _shakePos;
    //����ũ ���� (������ �Ⱦ���)
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
    public IEnumerator CameraShake(float _shakeTime, float _shakePos, float _shakeRot) //�ð�, ��ġ, ����
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
                    //PerlinNoise = ���� �Լ�  z�� �������� �������� �ϱ� ������ z�� ����.
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
