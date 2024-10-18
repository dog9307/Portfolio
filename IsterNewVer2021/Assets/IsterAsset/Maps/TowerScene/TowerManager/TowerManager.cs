using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TowerID
{
    NONE = -1,
    TowerLobby,
    TowerF1,
    TowerF2,
    TowerF3,
    TowerF4,
    TowerF5,
    END
}

public class TowerManager : PlayerAreaFinder
{
    public override string areaBaseName { get; } = "Tower";

    private List<int> _stages = new List<int>();
    private int _currentStageIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        //_currentArea = "TowerLobby";
        //SceneManager.LoadScene(_currentArea, LoadSceneMode.Additive);

        for (int i = (int)TowerID.NONE + 1; i < (int)TowerID.END; ++i)
            _stages.Add(i);
        //CommonFuncs.ShuffleList<int>(_stages);

        _currentStageIndex = 0;
    }

    public void EnterStage()
    {
        _currentStageIndex = 0;
        OpenStage();
    }

    public void StageClear()
    {
        _currentStageIndex++;
        if (_currentStageIndex < _stages.Count)
        {
            PlayerMapChanger mapChanger = FindObjectOfType<PlayerMapChanger>();
            if (mapChanger)
            {
                mapChanger.ChangeAreaBlackmaskPause(_currentStageIndex, "TowerStartField", 0);
            }

            //BlackMaskController.instance.AddEvent(OpenStage, BlackMaskEventType.MIDDLE);
            //BlackMaskController.instance.StartFading();
        }
    }

    public void OpenStage()
    {
        SceneManager.UnloadSceneAsync(_currentArea);
        _currentArea = "TowerF" + _stages[_currentStageIndex].ToString();
        SceneManager.LoadScene(_currentArea, LoadSceneMode.Additive);
    }

    public void ExitTower()
    {
        SceneManager.UnloadSceneAsync(_currentArea);
    }

    public override void ChangeArea(int nextAreaID, string nextFieldName, int nextStartPosID)
    {
        if (_currentArea.Contains(areaBaseName))
            SceneManager.UnloadSceneAsync(_currentArea);

        _currentArea = PlayerMapChanger.TowerName((TowerID)nextAreaID);

        if ((TowerID)nextAreaID == TowerID.TowerLobby)
            _currentStageIndex = 0;

        _isLoad = false;

        _nextFieldName = nextFieldName;
        _nextStartPosID = nextStartPosID;

        _op = SceneManager.LoadSceneAsync(_currentArea, LoadSceneMode.Additive);
        StartCoroutine(WaitForLoadArea());
    }

    public override void ChangeAreaWithBlackmaskPause(int nextAreaID, string nextFieldName, int nextStartPosID)
    {
        if (_currentArea.Contains(areaBaseName))
            SceneManager.UnloadSceneAsync(_currentArea);

        _currentArea = PlayerMapChanger.TowerName((TowerID)nextAreaID);

        _isLoad = false;

        _nextFieldName = nextFieldName;
        _nextStartPosID = nextStartPosID;

        _op = SceneManager.LoadSceneAsync(_currentArea, LoadSceneMode.Additive);
        StartCoroutine(WaitForLoadArea(true));
    }
}
