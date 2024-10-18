using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowLavaController : MonoBehaviour
{
    [SerializeField]
    private float _totalTime;

    void Start()
    {
        EventTimer timer = gameObject.AddComponent<EventTimer>();

        timer.AddEvent(Destroy);
        timer.totalTime = _totalTime;
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
