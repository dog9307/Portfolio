using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LocalizationManager : MonoBehaviour
{
    #region singleton
    static private LocalizationManager _instance;
    static public LocalizationManager instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = FindObjectOfType<LocalizationManager>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "SoundSystem";
                _instance = container.AddComponent<LocalizationManager>();
            }
        }
        DontDestroyOnLoad(LocalizationManager.instance);
    }
    #endregion singleton

    private string _currentLocalIdentifier;
    public string currentLocalCode { get { return _currentLocalIdentifier; } }

    void Start()
    {
        //_currentLocalIdentifier = PlayerPrefs.GetString("LastLanguageIdentifier", "ko-KR");
        //_currentLocalIdentifier = PlayerPrefs.GetString("LastLanguageIdentifier", "en-US");
        _currentLocalIdentifier = "ko-KR";
        LoadLocale(_currentLocalIdentifier);
    }

    // test
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.F1))
    //    {
    //        LoadLocale("en-US");
    //    }
    //    if (Input.GetKeyDown(KeyCode.F2))
    //    {
    //        LoadLocale("ko-KR");
    //    }
    //}

    public void LoadLocale(string languageIdentifier)
    {
        LocaleIdentifier localeCode = new LocaleIdentifier(languageIdentifier);
        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; i++)
        {
            Locale aLocale = LocalizationSettings.AvailableLocales.Locales[i];
            LocaleIdentifier anIdentifier = aLocale.Identifier;
            if (anIdentifier == localeCode)
            {
                _currentLocalIdentifier = aLocale.Identifier.Code;
                PlayerPrefs.SetString("LastLanguageIdentifier", _currentLocalIdentifier);

                LocalizationSettings.SelectedLocale = aLocale;
            }
        }
    }
}
