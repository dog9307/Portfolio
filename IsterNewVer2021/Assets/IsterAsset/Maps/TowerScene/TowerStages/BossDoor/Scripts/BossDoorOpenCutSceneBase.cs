using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossDoorOpenCutSceneBase : MonoBehaviour
{
    public virtual void StartOpenCutScene()
    {
        ReadyCutScene();
        StartCoroutine(CutScene());
    }

    protected abstract void ReadyCutScene();
    protected abstract IEnumerator CutScene();
}
