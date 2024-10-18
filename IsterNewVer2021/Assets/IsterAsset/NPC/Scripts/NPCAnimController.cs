using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimController : AnimController
{
    void OnEnable()
    {
        _anim.SetFloat("dirY", -1.0f);
    }
}
