using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DollyTrackOrthographicSizeController : MonoBehaviour
{
    [SerializeField]
    private float _minOrthoSize = 6.0f;
    [SerializeField]
    private float _maxOrthoSize = 16.0f;

    [SerializeField]
    private CinemachineVirtualCamera _vCam;
    private CinemachineTrackedDolly _trackedDolly;

    void Start()
    {
        if (!_vCam)
            Destroy(this);

        _trackedDolly = _vCam.GetCinemachineComponent<CinemachineTrackedDolly>();
        if (!_trackedDolly)
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        float ratio = _trackedDolly.m_PathPosition;
        float ortho = Mathf.Lerp(_minOrthoSize, _maxOrthoSize, ratio);

        _vCam.m_Lens.OrthographicSize = ortho;
    }
}
