using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DynamicRoomRange : MonoBehaviour
{
    private PolygonCollider2D _collider;

    private List<Vector2> _startPoints = new List<Vector2>();
    private List<Vector2> _currentPoints = new List<Vector2>();

    void Start()
    {
        _collider = GetComponent<PolygonCollider2D>();

        _collider.GetPath(0, _startPoints);
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 scale = transform.lossyScale;

        _collider.GetPath(0, _currentPoints);
        float startWidth = (_startPoints[3].x - _startPoints[0].x) * scale.x;
        float startHeight = (_startPoints[0].y - _startPoints[1].y) * scale.y;

        float orthoSize = Camera.main.orthographicSize;
        float screenHeight = orthoSize * 2.0f;
        float screenWidth = ((float)Screen.width * screenHeight) / (float)Screen.height;

        float targetX = ((screenWidth > startWidth ? screenWidth : startWidth) / 2.0f) / scale.x + 0.2f;
        float targetY = ((screenHeight > startHeight ? screenHeight : startHeight) / 2.0f) / scale.y + 0.2f;

        _currentPoints[0] = new Vector2(-targetX,  targetY);
        _currentPoints[1] = new Vector2(-targetX, -targetY);
        _currentPoints[2] = new Vector2( targetX, -targetY);
        _currentPoints[3] = new Vector2( targetX,  targetY);

        _collider.SetPath(0, _currentPoints);
    }
}
