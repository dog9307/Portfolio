using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMapChanger : SavableObject
{
    [SerializeField]
    private PlayerMoveController _player;

    private string _currentMap = "";
    public string currentMap { get { return _currentMap; } }
    private string _nextMap = "";

    public static bool isFindStartPos = false;

    private bool _isAlreadyChanging = false;

    void Start()
    {
        string currentMap = PlayerPrefs.GetString("PlayerCurrentMap", "UndergroundSceneTutorial");
        LoadMap(currentMap);
    }

    public void PlayerFreezing()
    {
        _isAlreadyChanging = true;
        _player.PlayerMoveFreeze(true);
    }

    public void PlayerUnfreezing()
    {
        _isAlreadyChanging = false;
        _player.PlayerMoveFreeze(false);
    }

    private GameObject _nextField;
    private int _nextStartPosID;
    public void ChangeField(GameObject nextField, int nextStartPosID, float fadeOutTime = 0.5f, float fadeInTime = 0.5f)
    {
        if (_isAlreadyChanging) return;

        _nextField = nextField;
        _nextStartPosID = nextStartPosID;

        BlackMaskController.instance.AddEvent(PlayerFreezing, BlackMaskEventType.PRE);
        BlackMaskController.instance.AddEvent(OpenField, BlackMaskEventType.MIDDLE);
        BlackMaskController.instance.AddEvent(PlayerUnfreezing, BlackMaskEventType.POST);
        BlackMaskController.instance.StartFading(fadeOutTime, fadeInTime);
    }

    public void OpenField()
    {
        FieldManager fieldManager = FindObjectOfType<FieldManager>();
        if (fieldManager)
            fieldManager.ChangeField(_nextField, _nextStartPosID);
    }

    public void ChangeMap(string mapName, float fadeOutTime = 0.5f, float fadeInTime = 0.5f)
    {
        if (_isAlreadyChanging) return;

        _nextMap = mapName;

        if (mapName == "WorldScene")
            _currentType = TeleportSceneType.World;
        else if (mapName == "TowerScene")
            _currentType = TeleportSceneType.Tower;
        else
            _currentType = TeleportSceneType.NONE;

        _isLoad = true;

        BlackMaskController.instance.AddEvent(PlayerFreezing, BlackMaskEventType.PRE);
        BlackMaskController.instance.AddEvent(OpenMap, BlackMaskEventType.MIDDLE);
        BlackMaskController.instance.AddEvent(PlayerUnfreezing, BlackMaskEventType.POST);
        BlackMaskController.instance.StartFading(fadeOutTime, fadeInTime);
    }

    private bool _isLoad = false;
    private int _nextAreaID;
    private string _nextFieldName;
    public void ChangeMap(string mapName, int nextAreaID, string nextFieldName, int nextStartPosID, float fadeOutTime = 0.5f, float fadeInTime = 0.5f)
    {
        if (_isAlreadyChanging) return;

        _nextMap = mapName;

        if (mapName == "WorldScene")
            _currentType = TeleportSceneType.World;
        else if (mapName == "TowerScene")
            _currentType = TeleportSceneType.Tower;
        else
            _currentType = TeleportSceneType.NONE;

        _isLoad = false;

        _nextAreaID = nextAreaID;
        _nextFieldName = nextFieldName;
        _nextStartPosID = nextStartPosID;

        BlackMaskController.instance.AddEvent(PlayerFreezing, BlackMaskEventType.PRE);
        BlackMaskController.instance.AddEvent(OpenMap, BlackMaskEventType.MIDDLE);
        BlackMaskController.instance.AddEvent(PlayerUnfreezing, BlackMaskEventType.POST);
        BlackMaskController.instance.StartFading(fadeOutTime, fadeInTime);
    }

    private AsyncOperation _op;
    public void OpenMap()
    {
        if (_currentMap.Length != 0)
            SceneManager.UnloadSceneAsync(_currentMap);
        _op = SceneManager.LoadSceneAsync(_nextMap, LoadSceneMode.Additive);

        _currentMap = _nextMap;

        StartCoroutine(WaitForMapLoad());
    }

    public void OpenArea()
    {
        PlayerAreaFinder areaFinder = FindObjectOfType<PlayerAreaFinder>();
        if (areaFinder)
            areaFinder.ChangeArea(_nextAreaID, _nextFieldName, _nextStartPosID);
    }

    public void OpenAreaBlackmaskPause()
    {
        PlayerAreaFinder areaFinder = FindObjectOfType<PlayerAreaFinder>();
        if (areaFinder)
            areaFinder.ChangeAreaWithBlackmaskPause(_nextAreaID, _nextFieldName, _nextStartPosID);
    }

    public void ChangeArea(int nextAreaID, string nextFieldName, int nextStartPosID, float fadeOutTime = 0.5f, float fadeInTime = 0.5f)
    {
        if (_isAlreadyChanging) return;

        _nextAreaID = nextAreaID;
        _nextFieldName = nextFieldName;
        _nextStartPosID = nextStartPosID;

        BlackMaskController.instance.AddEvent(PlayerFreezing, BlackMaskEventType.PRE);
        BlackMaskController.instance.AddEvent(OpenArea, BlackMaskEventType.MIDDLE);
        BlackMaskController.instance.AddEvent(PlayerUnfreezing, BlackMaskEventType.POST);
        BlackMaskController.instance.StartFading(fadeOutTime, fadeInTime);
    }

    public void ChangeAreaBlackmaskPause(int nextAreaID, string nextFieldName, int nextStartPosID, float fadeOutTime = 0.5f, float fadeInTime = 0.5f)
    {
        if (_isAlreadyChanging) return;

        _nextAreaID = nextAreaID;
        _nextFieldName = nextFieldName;
        _nextStartPosID = nextStartPosID;

        BlackMaskController.instance.AddEvent(PlayerFreezing, BlackMaskEventType.PRE);
        BlackMaskController.instance.AddEvent(OpenAreaBlackmaskPause, BlackMaskEventType.MIDDLE);
        BlackMaskController.instance.AddEvent(PlayerUnfreezing, BlackMaskEventType.POST);
        BlackMaskController.instance.StartFading(fadeOutTime, fadeInTime);
    }

    IEnumerator WaitForMapLoad()
    {
        BlackMaskController.instance.Pause();

        isFindStartPos = false;

        while (!_op.isDone)
            yield return null;

        PlayerAreaFinder areaFinder = FindObjectOfType<PlayerAreaFinder>();
        if (areaFinder)
        {
            if (_isLoad)
                areaFinder.LoadArea();
            else
                areaFinder.ChangeArea(_nextAreaID, _nextFieldName, _nextStartPosID);
        }

        while (!isFindStartPos)
            yield return null;

        AmbientChanger ambChanger = FindObjectOfType<AmbientChanger>();
        if (ambChanger)
            AmbientChanger.isSkipOnce = true;

        BGMChanger bgmChanger = FindObjectOfType<BGMChanger>();
        if (bgmChanger)
            BGMChanger.isSkipOnce = true;

        BlackMaskController.instance.Resume();
    }

    private TeleportSceneType _currentType;
    public void MinimapTeleport(TeleportSceneType nextType, int areaID, string fieldName, int startPosID, bool isTutorialEnd = false)
    {
        if (_currentType == nextType)
        {
            if (_currentType == TeleportSceneType.World)
                ChangeArea(areaID, fieldName, startPosID);
        }
        else
        {
            if (nextType == TeleportSceneType.Tower)
            {
                WorldMapManager world = FindObjectOfType<WorldMapManager>();
                if (world)
                    BlackMaskController.instance.AddEvent(world.ExitCurrentMap, BlackMaskEventType.MIDDLE);

                ChangeMap("TowerScene", areaID, fieldName, startPosID);
            }
            else if (nextType == TeleportSceneType.World)
            {
                UndergroundTutorialMap tutorial = FindObjectOfType<UndergroundTutorialMap>();
                if (tutorial && !isTutorialEnd)
                {
                    FieldManager fieldManager = FindObjectOfType<FieldManager>();

                    GameObject nextField = fieldManager.FindFieldByName(fieldName);
                    ChangeField(nextField, startPosID);
                }
                else
                {
                    TowerManager tower = FindObjectOfType<TowerManager>();
                    if (tower)
                        BlackMaskController.instance.AddEvent(tower.ExitCurrentMap, BlackMaskEventType.MIDDLE);

                    ChangeMap("WorldScene", areaID, fieldName, startPosID);
                }
            }
        }
    }

    public void LoadMap(string nextMap)
    {
        ChangeMap(nextMap, 0.0f);
    }

    public override SavableNode[] GetSaveNodes()
    {
        SavableNode[] nodes = new SavableNode[1];
        nodes[0] = new SavableNode();

        nodes[0].key = _key;
        nodes[0].value = _currentMap;

        return nodes;
    }

    public static string AreaName(AreaID id)
    {
        string areaName = "Area_" + ((int)id).ToString();
        return areaName;
    }

    public static string TowerName(TowerID id)
    {
        if (id == TowerID.TowerLobby)
            return id.ToString();

        string areaName = "TowerF" + ((int)id).ToString();
        return areaName;
    }

    public void TeleportedCutSceneStart()
    {
        StartCoroutine(WaitForTeleport());
    }

    IEnumerator WaitForTeleport()
    {
        while (!isFindStartPos)
            yield return null;

        yield return new WaitForEndOfFrame();

        HugeCrackTalkFrom crack = FindObjectOfType<HugeCrackTalkFrom>();
        if (crack)
            crack.TeleportedCutSceneStart();
    }
}
