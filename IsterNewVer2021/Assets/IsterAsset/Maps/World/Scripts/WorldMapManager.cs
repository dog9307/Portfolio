using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum AreaID
{
    NONE = -1,
    Middle,
    Area1,
    Area2,
    Area3,
    Area4,
    Area5,
    Underground,
    END
}

public class WorldMapManager : PlayerAreaFinder
{
}
