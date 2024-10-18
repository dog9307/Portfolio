using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossPatternBase : MonoBehaviour
{
    private BossController owner;
    public BossController _owner
    {
        get
        {
            if (!owner)
                owner = GetComponent<BossController>();
    
            return owner;
        }
        set { owner = value; }
    }

    [SerializeField]
    protected int _patternID;
    public int patternID { get { return _patternID; } }

    protected bool _patternEnd;
    public virtual bool isPatternEnd { get { return _patternEnd; } }

    public abstract void Start();
    public abstract void Update();

    public abstract void PatternStart();
    public abstract void PatternEnd();

    public abstract void PatternOn();
    public abstract void PatternOff();

    public Vector2 _dir { get; set; }

    [SerializeField]
    protected SFXPlayer _sfx;
    [SerializeField]
    protected string _patternStartSfx;
    [SerializeField]
    protected string _patternEndSfx;
    [SerializeField]
    protected string _patternOnSfx;
    [SerializeField]
    protected string _patternOffSfx;
}