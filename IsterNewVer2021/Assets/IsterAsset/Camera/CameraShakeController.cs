using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
 
public class CameraShakeController : MonoBehaviour
{
    #region SINGLETON
    static private CameraShakeController _instance;
    static public CameraShakeController instance { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<CameraShakeController>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "CameraShakeController";
                _instance = container.AddComponent<CameraShakeController>();
            }
        }
        else
            Destroy(gameObject);
    }
    #endregion

    public bool isSkip { get; set; }

    private CinemachineBrain _brain;
    private CinemachineVirtualCamera _currentCam;
    private CinemachineBasicMultiChannelPerlin _noise;

    private float _currentAmplitudeGain = 0.0f;

    private const float MAX_FIGURE = 100.0f;

    void Start()
    {
        _brain = GetComponent<CinemachineBrain>();
    }

    public void ChangeCamera()
    {
        if (!_brain)
            _brain = GetComponent<CinemachineBrain>();

        _currentCam = _brain.ActiveVirtualCamera as CinemachineVirtualCamera;
        if (_currentCam)
        {
            _noise = _currentCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (_noise)
            {
                _currentAmplitudeGain = 0.0f;
                _noise.m_AmplitudeGain = _currentAmplitudeGain;
            }
        }
    }

    public void CameraShake(float figure)
    {
        if (!_noise) return;

        figure = (figure > MAX_FIGURE ? MAX_FIGURE : figure);
        StartCoroutine(Shake(figure));
    }

    private int _coroutineCount = 0;
    [SerializeField]
    private float _totalShakeTime = 0.1f;
    IEnumerator Shake(float figure)
    {
        _coroutineCount++;
        float currentShakeTime = 0.0f;
        _currentAmplitudeGain = figure;
        while (currentShakeTime < _totalShakeTime)
        {
            float ratio = currentShakeTime / _totalShakeTime;

            _currentAmplitudeGain = Mathf.Lerp(figure, 0.0f, ratio);

            if (_noise)
                _noise.m_AmplitudeGain = _currentAmplitudeGain;
            else
                break;

            yield return null;

            currentShakeTime += IsterTimeManager.originDeltaTime;

            if (_coroutineCount > 1)
                break;
        }

        _currentAmplitudeGain = 0.0f;
        if (_noise)
            _noise.m_AmplitudeGain = _currentAmplitudeGain;

        _coroutineCount--;
    }
}
