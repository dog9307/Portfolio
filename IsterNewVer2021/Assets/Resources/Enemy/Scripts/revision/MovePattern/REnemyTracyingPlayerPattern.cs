using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REnemyTracyingPlayerPattern : REnemyMovePatternBase
{
    // Start is called before the first frame update
    void Start()
    {
        moveType = MP.tracking;
    }   
}
