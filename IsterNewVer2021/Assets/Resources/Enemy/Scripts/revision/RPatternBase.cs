using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class RPatternBase : MonoBehaviour
{
    private REnemyController owner;
    public REnemyController _owner
    {
        get
        {
            if (!owner)
                owner = GetComponent<REnemyController>();

            return owner;
        }
        set { owner = value; }
    }

    protected bool _patternEnd;
    public virtual bool isPatternEnd { get { return _patternEnd; } }
  
    public abstract void Init();
    public abstract void Update();

    public abstract void PatternStart();
    public abstract void PatternEnd();
    
    public Vector2 _dir { get; set; }       
}
