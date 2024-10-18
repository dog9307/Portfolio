using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct SceneLoadData
{
    public string sceneName;
    public LoadSceneMode targetMode;
}

public class SceneLoader : MonoBehaviour
{
    #region SINGLETON
    static private SceneLoader _instance;
    static public SceneLoader instance { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<SceneLoader>();
            if (!_instance)
                return;

            DontDestroyOnLoad(SceneLoader.instance);
        }
        else
            Destroy(gameObject);
    }
    #endregion

    private Queue<SceneLoadData> _sceneDatas = new Queue<SceneLoadData>();
    private AsyncOperation _currentOperation;

    public void Release()
    {
        _sceneDatas.Clear();
    }

    public void AddReadyToLoadScene(SceneLoadData data)
    {
        _sceneDatas.Enqueue(data);
    }

    public void StartSceneLoad()
    {
        ScreenMaskController.instance.AddMaskingEvent(LoadStart, MaskingEventTiming.Middle);
        ScreenMaskController.instance.SetMaskingCondition(IsSceneChangeDone);
        ScreenMaskController.instance.StartFade();
    }

    public void LoadStart()
    {
        StartCoroutine(Load());
    }

    public bool IsSceneChangeDone()
    {
        if (_sceneDatas.Count <= 0)
        {
            foreach (var o in _operations)
            {
                if (!o.isDone)
                    return false;
            }

            return true;
        }

        return false;
    }

    private List<AsyncOperation> _operations = new List<AsyncOperation>();
    IEnumerator Load()
    {
        while (_sceneDatas.Count > 0)
        {
            yield return null;

            while (_sceneDatas.Count > 0)
            {
                SceneLoadData data = _sceneDatas.Dequeue();
                _currentOperation = SceneManager.LoadSceneAsync(data.sceneName, data.targetMode);

                _currentOperation.allowSceneActivation = false;

                _operations.Add(_currentOperation);
            }

            while (_currentOperation.progress < 0.9f)
                yield return null;

            foreach (var o in _operations)
                o.allowSceneActivation = true;

            while (!_currentOperation.isDone)
                yield return null;

            _currentOperation = null;
        }

        _sceneDatas.Clear();
    }
}
