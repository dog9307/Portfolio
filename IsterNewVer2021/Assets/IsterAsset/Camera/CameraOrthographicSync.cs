using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrthographicSync : MonoBehaviour
{
    private Camera _cam;

    // Update is called once per frame
    void Update()
    {
        if (!_cam)
            _cam = GetComponent<Camera>();

        _cam.orthographicSize = Camera.main.orthographicSize;
    }
}
