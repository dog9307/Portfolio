using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroController : MonoBehaviour
{
    [SerializeField]
    private ScenePasser _titlePasser;

    public void GoToTitle()
    {
        _titlePasser?.StartScenePass();
    }
}
