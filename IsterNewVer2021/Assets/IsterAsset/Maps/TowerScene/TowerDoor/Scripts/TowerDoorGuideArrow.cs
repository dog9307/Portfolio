using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDoorGuideArrow : MonoBehaviour
{
    [SerializeField]
    private Transform _guideCenter;
    private PlayerMoveController _player;

    [SerializeField]
    private float _yOffset = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerMoveController>();

        transform.rotation = Quaternion.identity;

        Vector2 dir = CommonFuncs.CalcDir(_player, _guideCenter);
        float angle = CommonFuncs.DirToDegree(dir);

        transform.position = _player.transform.position + (Vector3)dir * 4.0f;
        transform.Rotate(new Vector3(0.0f, 0.0f, angle));
    }

    // Update is called once per frame
    void Update()
    {
        if (!_player) return;

        if (CommonFuncs.Distance(_player.transform.position, _guideCenter.position) < 4.5f)
        {
            Vector2 newPos = _guideCenter.position;
            newPos.y += _yOffset;
            transform.position = newPos;

            Vector2 dir = CommonFuncs.CalcDir(transform, _guideCenter);
            float angle = CommonFuncs.DirToDegree(dir);

            transform.rotation = Quaternion.identity;
            transform.Rotate(new Vector3(0.0f, 0.0f, angle));
        }
        else
        {
            transform.rotation = Quaternion.identity;

            Vector2 dir = CommonFuncs.CalcDir(_player, _guideCenter);
            float angle = CommonFuncs.DirToDegree(dir);

            transform.position = _player.transform.position + (Vector3)dir * 4.0f;
            transform.Rotate(new Vector3(0.0f, 0.0f, angle));
        }
    }
}
