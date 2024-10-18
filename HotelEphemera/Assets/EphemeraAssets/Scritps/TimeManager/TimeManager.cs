using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeManager
{
    public static float timeScale { get; set; } = 1.0f;

    public static float originDeltaTime { get { return Time.deltaTime; } }

    public static float deltaTime { get { return Time.deltaTime * timeScale; } }
}
