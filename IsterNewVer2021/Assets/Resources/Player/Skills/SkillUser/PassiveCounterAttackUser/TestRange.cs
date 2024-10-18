using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRange : MonoBehaviour
{
    [SerializeField]
    private PassiveCounterAttackUser _user;

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = transform.localScale;
        scale.x = _user.maxDistance * 2.0f;
        scale.y = _user.maxDistance * 2.0f;
        transform.localScale = scale;
    }
}
