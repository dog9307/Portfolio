using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsterTimeManager : MonoBehaviour
{
    private static float _enemyTimeScale = 1.0f;
    public static float enemyTimeScale { get { return _enemyTimeScale * globalTimeScale; } set { _enemyTimeScale = value; } }
    private static float _bossTimeScale = 1.0f;
    public static float bossTimeScale { get { return _bossTimeScale * globalTimeScale; } set { _bossTimeScale = value; } }

    public static float originDeltaTime { get { return Time.deltaTime; } }
    public static float originFixedDeltaTime { get { return Time.fixedDeltaTime; } }
    public static float deltaTime { get { return Time.deltaTime * globalTimeScale; } }
    public static float fixedDeltaTime { get { return Time.fixedDeltaTime * globalTimeScale; } }
    public static float enemyDeltaTime { get { return Time.deltaTime * enemyTimeScale; } }
    public static float bossDeltaTime { get { return Time.deltaTime * bossTimeScale; } }

    private static float _globalTimeScale = 1.0f;
    public static float globalTimeScale { get { return _globalTimeScale; } set { _globalTimeScale = value; } }
}
