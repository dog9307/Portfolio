using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavableNode
{
    public string key;
    public object value;

    public void Save()
    {
        if (typeof(int).IsInstanceOfType(value))
            PlayerPrefs.SetInt(key, (int)value);
        else if (typeof(float).IsInstanceOfType(value))
            PlayerPrefs.SetFloat(key, (float)value);
        else if (typeof(string).IsInstanceOfType(value))
            PlayerPrefs.SetString(key, (string)value);
    }
}

public class SavableDataManager : MonoBehaviour
{
    #region SINGLETON
    static private SavableDataManager _instance;
    static public SavableDataManager instance { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<SavableDataManager>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "SavableDataManager";
                _instance = container.AddComponent<SavableDataManager>();
            }
        }
        else
            Destroy(gameObject);
    }
    #endregion

    private List<SavableNode> _saveList = new List<SavableNode>();

    //void Start()
    //{
    //    //string targetMap = PlayerPrefs.GetString("PlayerDieMap", "");
    //    //if (targetMap == "WorldScene" ||
    //    //    targetMap == "UndergroundSceneTutorial")
    //    //{
    //    //    if (SavableDataManager.instance)
    //    //        SavableDataManager.instance.CreateDarkLight();
    //    //}
    //}

    // test
#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            if (Input.GetKey(KeyCode.LeftControl))
                SaveList();
        }
    }
#endif

    public void AddSavableObject(SavableNode save, bool isMustSave = false)
    {
        if (isMustSave)
            save.Save();
        else
            _saveList.Add(save);
    }

    //public void AddSavableObject(SavableObject savable, bool isMustSave = false)
    //{
    //    SavableNode[] nodes = savable.GetSaveNodes();
    //    foreach (var save in nodes)
    //    {
    //        if (isMustSave)
    //            save.Save();
    //        else
    //            _saveList.Add(save);
    //    }
    //}

    public void SaveList()
    {
        foreach (var s in _saveList)
            s.Save();

        _saveList.Clear();

        //PlayerPrefs.SetInt("PlayerAlreadyStart", 100);
    }

    //public void TowerLobbySave(int nextStartPosID)
    //{
    //    PlayerSkillUsage playerSkill = FindObjectOfType<PlayerSkillUsage>();
    //    if (playerSkill)
    //    {
    //        ActiveItemRerollUser healUser = playerSkill.FindUser<ActiveItemReroll>() as ActiveItemRerollUser;
    //        if (healUser)
    //            healUser.ChargeAll();

    //        AddSavableObject(playerSkill);
    //    }

    //    PlayerMapChanger mapChanger = FindObjectOfType<PlayerMapChanger>();
    //    if (mapChanger)
    //        AddSavableObject(mapChanger);

    //    PlayerAreaFinder area = FindObjectOfType<PlayerAreaFinder>();
    //    if (area)
    //        AddSavableObject(area);

    //    FieldManager field = FindObjectOfType<FieldManager>();
    //    if (field)
    //        AddSavableObject(field);

    //    SavableNode node = new SavableNode();

    //    node.key = "PlayerNextStartPos";
    //    node.value = nextStartPosID;

    //    AddSavableObject(node);

    //    Damagable damagable = playerSkill.GetComponent<Damagable>();
    //    if (damagable)
    //    {
    //        node = new SavableNode();

    //        node.key = "PlayerTotalHP";
    //        node.value = damagable.totalHP;

    //        AddSavableObject(node);

    //        damagable.Heal(damagable.totalHP);
    //    }

    //    RelicIconList relicIcon = FindObjectOfType<RelicIconList>();
    //    if (relicIcon)
    //        AddSavableObject(relicIcon);

    //    PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
    //    if (inventory)
    //        AddSavableObject(inventory);

    //    PlayerPassiveSkillStorage passiveStorage = FindObjectOfType<PlayerPassiveSkillStorage>();
    //    if (passiveStorage)
    //        AddSavableObject(passiveStorage);

    //    SaveList();
    //}

    //public void PlayerDieSave()
    //{
    //    _saveList.Clear();

    //    SavableNode node = new SavableNode();

    //    node.key = "PlayerDie";
    //    node.value = 100;

    //    AddSavableObject(node);

    //    PlayerMapChanger mapChanger = FindObjectOfType<PlayerMapChanger>();
    //    if (mapChanger)
    //    {
    //        node = new SavableNode();

    //        node.key = "PlayerDieMap";
    //        node.value = mapChanger.currentMap;

    //        AddSavableObject(node);

    //        if (mapChanger.currentMap.Contains("World") ||
    //            mapChanger.currentMap.Contains("Underground"))
    //        {

    //            PlayerAreaFinder area = FindObjectOfType<PlayerAreaFinder>();
    //            if (area)
    //            {
    //                node = new SavableNode();

    //                node.key = "PlayerDieArea";
    //                node.value = area.currentArea;

    //                AddSavableObject(node);
    //            }

    //            FieldManager field = FindObjectOfType<FieldManager>();
    //            if (field)
    //            {
    //                node = new SavableNode();

    //                node.key = "PlayerDieField";
    //                node.value = field.currentField.name;

    //                AddSavableObject(node);
    //            }

    //            //node = new SavableNode();

    //            //node.key = "PlayerNextStartPos";
    //            //node.value = 9000;

    //            //AddSavableObject(node);

    //            PlayerInventory player = FindObjectOfType<PlayerInventory>();
    //            if (player)
    //            {
    //                Vector3 pos = player.transform.position;

    //                node = new SavableNode();

    //                node.key = "PlayerDieX";
    //                node.value = pos.x;

    //                AddSavableObject(node);

    //                node = new SavableNode();

    //                node.key = "PlayerDieY";
    //                node.value = pos.y;

    //                AddSavableObject(node);

    //                node = new SavableNode();

    //                node.key = "PlayerDieDarkLight";
    //                node.value = player.darkLight;

    //                AddSavableObject(node);
    //            }
    //        }
    //        else if (mapChanger.currentMap.Contains("Tower"))
    //        {
    //            PlayerInventory player = FindObjectOfType<PlayerInventory>();
    //            if (player)
    //                AddSavableObject(player);

    //            Damagable damagable = player.GetComponent<Damagable>();
    //            if (damagable)
    //            {
    //                node = new SavableNode();

    //                node.key = "PlayerTotalHP";
    //                node.value = damagable.totalHP;

    //                AddSavableObject(node);
    //            }
    //        }
    //    }

    //    SaveList();
    //}

    //[SerializeField]
    //private GameObject _darkLight;
    //public GameObject effectPrefab { get { return _darkLight; } set { _darkLight = value; } }
    //public GameObject CreateObject()
    //{
    //    GameObject newDarkLight = Instantiate(effectPrefab);

    //    float x = PlayerPrefs.GetFloat("PlayerDieX", 0.0f);
    //    float y = PlayerPrefs.GetFloat("PlayerDieY", 0.0f);
    //    float z = 0.0f;

    //    newDarkLight.transform.position = new Vector3(x, y, z);

    //    return newDarkLight;
    //}

    //public void CreateDarkLight()
    //{
    //    StartCoroutine(CheckCorrectMap());
    //}

    //IEnumerator CheckCorrectMap()
    //{
    //    PlayerMapChanger playerMapChanger = null;
    //    PlayerAreaFinder areaFinder = null;
    //    FieldManager fieldManager = null;

    //    string targetMap = PlayerPrefs.GetString("PlayerDieMap", "");
    //    string targetArea = PlayerPrefs.GetString("PlayerDieArea", "");
    //    string targetField = PlayerPrefs.GetString("PlayerDieField", "");
    //    while (true)
    //    {
    //        if (!playerMapChanger)
    //            playerMapChanger = FindObjectOfType<PlayerMapChanger>();
    //        if (!areaFinder)
    //            areaFinder = FindObjectOfType<PlayerAreaFinder>();
    //        if (!fieldManager)
    //            fieldManager = FindObjectOfType<FieldManager>();

    //        if (playerMapChanger.currentMap == targetMap &&
    //            areaFinder.currentArea == targetArea &&
    //            fieldManager.currentField.name == targetField)
    //        {
    //            GameObject newDarkLight = CreateObject();
    //            newDarkLight.transform.parent = fieldManager.currentField.transform;

    //            int dieCount = PlayerPrefs.GetInt("PlayerDieDarkLight", 0);
    //            DarkLightTalkFrom darkLight = newDarkLight.GetComponent<DarkLightTalkFrom>();
    //            if (darkLight)
    //            {
    //                darkLight.figure = dieCount / 2;
    //                PlayerPrefs.SetInt("PlayerDieDarkLight", 0);
    //            }

    //            break;
    //        }

    //        yield return null;
    //    }
    //}

    public int FindIntSavableData(string key)
    {
        int count = -1000;
        count = PlayerPrefs.GetInt(key, -1000);
        if (count >= 100)
            return count;

        foreach (var d in _saveList)
        {
            if (d.key == key)
                return (int)d.value;
        }

        return count;
    }
}
