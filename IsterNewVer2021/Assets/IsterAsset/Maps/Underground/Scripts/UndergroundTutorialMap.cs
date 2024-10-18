using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UndergroundTutorialMap : PlayerAreaFinder
{
    public override void LoadArea()
    {
        base.LoadArea();
        StartCoroutine(StartTutorial());
    }

    private void OnDestroy()
    {
        SceneManager.UnloadSceneAsync("Area_6");
    }

    IEnumerator StartTutorial()
    {
        while (!_op.isDone)
            yield return null;

        SavePointController[] savePoints = FindObjectsOfType<SavePointController>(true);
        foreach (var s in savePoints)
            s.OnSave.AddListener(SaveChaninEnd);
    }

    public void SaveChaninEnd()
    {
        PlayerPrefs.SetInt("UndergroundChainDone", 100);
    }
}
