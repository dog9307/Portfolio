using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class PlayerAreaFinder : SavableObject
{
    public virtual string areaBaseName { get; } = "Area_";

    protected AsyncOperation _op;
    protected string _currentArea = "";
    public string currentArea { get { return _currentArea; } }

    protected bool _isLoad;
    protected string _nextFieldName;
    protected int _nextStartPosID;

    public virtual void ChangeArea(string nextArea)
    {
        if (_currentArea.Contains(areaBaseName))
            SceneManager.UnloadSceneAsync(_currentArea);

        _currentArea = nextArea;

        _isLoad = true;

        _op = SceneManager.LoadSceneAsync(_currentArea, LoadSceneMode.Additive);
        StartCoroutine(WaitForLoadArea());
    }

    public virtual void ChangeArea(int nextAreaID, string nextFieldName, int nextStartPosID)
    {
        if (_currentArea.Contains(areaBaseName))
            SceneManager.UnloadSceneAsync(_currentArea);

        _currentArea = PlayerMapChanger.AreaName((AreaID)nextAreaID);

        _isLoad = false;

        _nextFieldName = nextFieldName;
        _nextStartPosID = nextStartPosID;

        _op = SceneManager.LoadSceneAsync(_currentArea, LoadSceneMode.Additive);
        StartCoroutine(WaitForLoadArea());
    }

    public virtual void ChangeAreaWithBlackmaskPause(int nextAreaID, string nextFieldName, int nextStartPosID)
    {
        if (_currentArea.Contains(areaBaseName))
            SceneManager.UnloadSceneAsync(_currentArea);

        _currentArea = PlayerMapChanger.AreaName((AreaID)nextAreaID);

        _isLoad = false;

        _nextFieldName = nextFieldName;
        _nextStartPosID = nextStartPosID;

        _op = SceneManager.LoadSceneAsync(_currentArea, LoadSceneMode.Additive);
        StartCoroutine(WaitForLoadArea(true));
    }

    public virtual void LoadArea()
    {
#if UNITY_EDITOR
        string nextArea = PlayerPrefs.GetString("PlayerCurrentArea", "Area_6");
        ChangeArea(nextArea);
#else
        string nextArea = PlayerPrefs.GetString("PlayerCurrentArea", "Area_6");
        ChangeArea(nextArea);
#endif
    }

    protected IEnumerator WaitForLoadArea(bool isBlackMaskPause = false)
    {
        if (isBlackMaskPause)
            BlackMaskController.instance.Pause();

        while (!_op.isDone)
            yield return null;

        FieldManager fieldManager = FindObjectOfType<FieldManager>();
        if (fieldManager)
        {
            if (_isLoad)
                fieldManager.LoadField();
            else
                fieldManager.ChangeField(_nextFieldName, _nextStartPosID);
        }
        else
        {
            PlayerStartPosFinder startPosFinder = FindObjectOfType<PlayerStartPosFinder>();
            if (startPosFinder)
            {
                startPosFinder.nextStartPosID = _nextStartPosID;
                startPosFinder.FindStartPos(_isLoad);
            }
        }

        if (isBlackMaskPause)
            BlackMaskController.instance.Resume();
    }

    public void ExitCurrentMap()
    {
        SceneManager.UnloadSceneAsync(_currentArea);
    }

    public override SavableNode[] GetSaveNodes()
    {
        SavableNode[] nodes = new SavableNode[1];
        nodes[0] = new SavableNode();

        nodes[0].key = "PlayerCurrentArea";
        nodes[0].value = _currentArea;

        return nodes;
    }
}