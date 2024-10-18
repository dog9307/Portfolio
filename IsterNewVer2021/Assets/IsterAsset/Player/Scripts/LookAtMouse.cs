using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    public Vector2 dir { get; set; }

    private PlayerMoveController _move;

    public bool isEnable { get; set; }

    // Start is called before the first frame update
    void Start()
    {
       //Cursor.lockState = CursorLockMode.Confined;

        _move = GetComponent<PlayerMoveController>();

        //isEnable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsCanLookAt()) return;

        CalcDir();
    }

    public void LookReset()
    {
        dir = Vector2.up;
    }

    public void CalcDir()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 mousePos = Input.mousePosition;
        dir = (mousePos - screenPos).normalized;
    }

    private bool IsCanLookAt()
    {
        PlayerSkillUsage skill = _move.GetComponent<PlayerSkillUsage>();
        bool isSkillUsing = skill.isSkillUsing;

        ActiveParryingUser parrying = skill.FindUser<ActiveParrying>() as ActiveParryingUser;
        if (parrying)
        {
            if (isSkillUsing && parrying.isParryingReady)
                isSkillUsing = false;
        }

        return (!_move.isAttacking && !isSkillUsing && !_move.isDelay && isEnable);
    }
}
