using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatternFactory : MonoBehaviour
{
    public REnemyController _controller;

    public List<REnemyAttackPatternBase> attackpattern {  get { if (_controller) return _controller.attackpattern; return null; } }
    public List<REnemyMovePatternBase> movepattern { get { if (_controller) return _controller.movePattern; return null; } }

    void Start()
    {
        Init();
    }

    public void Release()
    {
        foreach (var pattern in attackpattern)
        {
            DestroyImmediate(pattern);
            DestroyImmediate(pattern._creator);
        }

        foreach (var pattern in movepattern)
        {
            DestroyImmediate(pattern);
        }
        _controller.attackpattern.Clear();
        _controller.movePattern.Clear();
    }

    public void Release(REnemyAttackPatternBase remove)
    {
        attackpattern.Remove(remove);
        DestroyImmediate(remove);
    }
    public void Release(REnemyMovePatternBase remove)
    {
        movepattern.Remove(remove);
        DestroyImmediate(remove);
    }

    public void Init()
    {
        _controller = GetComponent<REnemyController>();
    }

      
}
