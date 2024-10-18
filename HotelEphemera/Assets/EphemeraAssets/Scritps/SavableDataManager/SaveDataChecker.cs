using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Michsky.UI.Dark;

public class SaveDataChecker : MonoBehaviour
{
    [SerializeField]
    private ScenePasser _passer;

    [SerializeField]
    private Button _loadButton;

    [SerializeField]
    private ModalWindowManager _modal;

    void Start()
    {
        if (!_loadButton) return;

        int count = SavableDataManager.instance.FindIntSavableData("FirstRun");
        _loadButton.interactable = (count >= 100);
    }
    
    public void SaveDataCheck()
    {
        if (!_loadButton || !_passer || !_modal) return;

        if (_loadButton.interactable)
            _modal.ModalWindowIn();
        else
        {
            ScreenMaskController.instance.isPause = true;
            _passer.StartScenePass();
        }
    }

    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
