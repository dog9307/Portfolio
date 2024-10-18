using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AP { rangebasic, meleebasic, rangehoming }

public enum MP { tracking, runaway, teleport, dash , fix}

public static class ReFactory
{
    public static REnemyAttackPatternBase GetPattern(AP attackpattern, GameObject target)
    {
        REnemyAttackPatternBase newPattern = null;
        switch (attackpattern)
        {
            case AP.rangebasic:
                newPattern = target.AddComponent<REnemyRangeAttack>();
                //newPattern.name = "Range";
                break;
            case AP.meleebasic:
                newPattern = target.AddComponent<REnemyMeleeAttack>();
                //newPattern.name = "Melee";
                break;
            case AP.rangehoming:
                newPattern = target.AddComponent<REnemyRangeAttack>();
                break;
            default:
                break;
        }

        newPattern.Init();

        return newPattern;
    }

    public static REnemyMovePatternBase GetMovePattern(MP movePattern, GameObject target)
    {
        REnemyMovePatternBase newPattern = null;
        switch (movePattern)
        {
            case MP.tracking:
                newPattern = target.AddComponent<REnemyTracyingPlayerPattern>();
                break;
            case MP.runaway:
                newPattern = target.AddComponent<REnemyTracyingPlayerPattern>();
                break;
            case MP.teleport:
                newPattern = target.AddComponent<REnemyTracyingPlayerPattern>();
                break;
            case MP.dash:
                break;
            default:
                break;
        }
    
        newPattern.Init();
    
        return newPattern;
    }
    
}
