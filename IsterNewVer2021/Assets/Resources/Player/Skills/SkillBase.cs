using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase
{
    public SkillInfo owner { get; set; }

    public bool isReversePowerUp { get; set; }

    public Sprite skillIcon { get; set; }
    public string skillName { get; set; }
    public string skillDesc { get; set; }

    public string soundName { get; set; }

    public abstract int id { get; }

    public virtual void Init()
    {
        soundName = "";

        isReversePowerUp = false;

        string path = "Player/Skills/Icons/" + GetType().ToString();
        skillIcon = Resources.Load<Sprite>(path);
    }

    public abstract void UseSkill();
}
