using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectCreator
{
    GameObject effectPrefab { get; set; }

    GameObject CreateObject();
}
