using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    #region singleton
    static private DataManager _instance;
    static public DataManager instance { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<DataManager>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "DataManager";
                _instance = container.AddComponent<DataManager>();
            }
        }

        DontDestroyOnLoad(DataManager.instance);
    }
    //private static DataManager _instance;

    //public static DataManager Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = GameObject.FindObjectOfType(typeof(DataManager)) as DataManager;
    //            if (_instance == null)
    //            {
    //                GameObject dataManager = new GameObject();
    //                dataManager.name = "DataManager";
    //                _instance = dataManager.AddComponent<DataManager>();
    //            }
    //        }
    //        return _instance;
    //    }
    //}

    //private void Awake()
    //{
    //    DontDestroyOnLoad(this);
    //}
    #endregion singleton

    [SerializeField]
    public Dictionary<string, int> dataInfosInt = new Dictionary<string, int>(); //시스템 데이터의 값들 (해상도 기타등등)
    [SerializeField]
    public Dictionary<string, float> dataInfosfloat = new Dictionary<string, float>(); //시스템 데이터의 값들 (사운드 기타등등)
    [SerializeField]
    public Dictionary<string, bool> switchInfos = new Dictionary<string, bool>(); //스위치, 클리어 관련
    [SerializeField]
    
    private void Start()
    {
        if (!dataInfosInt.ContainsKey("ScreenX")) dataInfosInt.Add("ScreenX", PlayerPrefs.GetInt("ScreenX", 1920));
        if (!dataInfosInt.ContainsKey("ScreenY")) dataInfosInt.Add("ScreenY", PlayerPrefs.GetInt("ScreenY", 1080));
        if (!dataInfosfloat.ContainsKey("Brightness")) dataInfosfloat.Add("Brightness", PlayerPrefs.GetFloat("Brightness", 1.0f));
        if (!dataInfosfloat.ContainsKey("EffectVolume")) dataInfosfloat.Add("EffectVolume", PlayerPrefs.GetFloat("EffectVolume", 1.0f));
        if (!dataInfosfloat.ContainsKey("MasterVolume")) dataInfosfloat.Add("MasterVolume", PlayerPrefs.GetFloat("MasterVolume", 1.0f));
        if (!dataInfosfloat.ContainsKey("BgmVolume")) dataInfosfloat.Add("BgmVolume", PlayerPrefs.GetFloat("BgmVolume",1.0f));
        if (System.Convert.ToBoolean(PlayerPrefs.GetString("FullScreen", "true"))) switchInfos.Add("FullScreen", true);
        else switchInfos.Add("FullScreen", false);


        if (!dataInfosInt.ContainsKey("ResolutionNum")) dataInfosInt.Add("ResolutionNum", PlayerPrefs.GetInt("ResolutionNum", 5));
    }
    private void Update()
    {
    }

    void SettingDatas()
    {

    }

}
