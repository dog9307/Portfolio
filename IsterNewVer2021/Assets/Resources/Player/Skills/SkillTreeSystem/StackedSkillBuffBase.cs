using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StackedSkillBuffBase : SkillBuffBase
{
    public int currentLevel { get; set; }

    public void Upgrade()
    {
        ++currentLevel;
    }

    public void DownGrade()
    {
        --currentLevel;
        if (currentLevel < 0)
            currentLevel = 0;
    }
}
