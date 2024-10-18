using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryingShieldController : MonoBehaviour
{
    private LookAtMouse _look;

    public ActiveParryingUser owner { get; set; }
    public bool isParryingSuccess { get { return owner.isParryingSuccess; } set { owner.isParryingSuccess = value; } }

    void OnEnable()
    {
        if (!_look)
            _look = FindObjectOfType<LookAtMouse>();

        Update();
    }

    void OnDisable()
    {
        owner.SkillEnd();
    }

    void Update()
    {
        float angle = CommonFuncs.DirToDegree(_look.dir);
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        transform.Rotate(new Vector3(0.0f, 0.0f, angle));
    }

    public void EffectSpeedSetting(float totalTime)
    {
        Animator anim = GetComponentInChildren<Animator>();
        float multiplier = 1.0f / (totalTime * 2.0f);
        anim.SetFloat("timeMultiplier", multiplier);
    }

    public bool IsCorrectDirection(Vector2 targetDir)
    {
        float dot = Vector2.Dot(_look.dir, targetDir);

        return (dot < 0.0f);
    }
}
