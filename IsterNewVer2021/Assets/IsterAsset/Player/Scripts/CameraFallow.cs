using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFallow : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 1.0f)] private float _ratio;

    private float _currentTime;
    public float currentRatio { get { return _ratio; } set { _ratio = value; } }

    private Movable _move;
    private Damagable _damagable;

    public bool isDie { get { if (_damagable) return _damagable.isDie; else return false; } }

    void Start()
    {
        _move = FindObjectOfType<PlayerMoveController>();

        GameObject player = _move.gameObject;
        transform.parent = player.transform;
        currentRatio = _ratio;

        _damagable = player.GetComponent<Damagable>();

        _currentTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        CalcCurrentRatio();

        Vector2 screenPos = Camera.main.WorldToScreenPoint(_move.center);
        Vector2 mousePos = Input.mousePosition;
        Vector2 dir = (mousePos - screenPos) * currentRatio;

        Vector3 fallowPos = Camera.main.ScreenToWorldPoint((screenPos + dir));
        fallowPos.z = GetComponentInParent<Transform>().position.z;

        transform.position = fallowPos;
    }

    void CalcCurrentRatio()
    {
        if (!isDie) return;

        _currentTime += IsterTimeManager.deltaTime / 2.0f;
        if (_currentTime > 1.0f)
            _currentTime = 1.0f;
        currentRatio = Mathf.Lerp(_ratio, 0.0f, _currentTime);
    }
}
