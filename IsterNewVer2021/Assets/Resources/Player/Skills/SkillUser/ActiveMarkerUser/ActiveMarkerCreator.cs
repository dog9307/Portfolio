using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMarkerCreator : FloorSkillCreator
{
    // Start is called before the first frame update
    void Start()
    {
        effectPrefab = Resources.Load<GameObject>("Player/Bullets/ActiveMarker/Prefab/ActiveMarker");
    }
}
