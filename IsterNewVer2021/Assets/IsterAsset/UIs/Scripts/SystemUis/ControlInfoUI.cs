using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControlInfoUI : MonoBehaviour
{
    [SerializeField]
    InGameOptionUi _ingameOption;
    [SerializeField]
    StartSceneUI _startScene;

    public Button _exit;

    private void OnEnable()
    {
        if (_ingameOption)
        {
            _ingameOption = GetComponentInParent<InGameOptionUi>();
        }

        if (_startScene)
        {
            _startScene = GetComponentInParent<StartSceneUI>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_ingameOption)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && _ingameOption._subMenuOn)
            {
                _ingameOption.ControllerMenuOff();
            }
        }
        else if (_startScene)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && _startScene._optionSubMenuOn)
            {
                _startScene.ControllerMenuOff();
            }
        }
    }
}
