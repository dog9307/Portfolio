using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScenePasser : MonoBehaviour
{
    [SerializeField]
    private bool _isAnykeyPress = false;

    [SerializeField]
    private List<string> _nextScenes = new List<string>();

    void Update()
    {
        if (!_isAnykeyPress) return;

        if (Input.anyKeyDown)
            StartScenePass();
    }

    public void StartScenePass()
    {
        for (int i = 0; i < _nextScenes.Count; ++i)
        {
            SceneLoadData currentData = new SceneLoadData();
            currentData.sceneName = _nextScenes[i];
            if (i == 0)
                currentData.targetMode = LoadSceneMode.Single;
            else
                currentData.targetMode = LoadSceneMode.Additive;

            SceneLoader.instance.AddReadyToLoadScene(currentData);
        }

        SceneLoader.instance.StartSceneLoad();

        OnPassScene();
    }

    public UnityEvent onPassScene;
    public void OnPassScene()
    {
        if (onPassScene != null)
            onPassScene.Invoke();
    }
}
