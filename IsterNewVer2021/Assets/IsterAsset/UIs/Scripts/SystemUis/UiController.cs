using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[System.Serializable]
public struct UiBundle
{
    public GameObject _UiButton;
    public GameObject _UiPrefab;
}

public class UiController : MonoBehaviour
{
    public GameObject _prevUiButton;
    public GameObject _currentMenu;

    [SerializeField]
    List<UiBundle> _uiBundle;

    [HideInInspector]
    public bool _firstStart;
    //다른 메뉴 열려있을 때.
    public bool _subMenuOn;
    public bool _optionSubMenuOn;

    [SerializeField]
    private SFXPlayer _sfx;

    private void Start()
    {
        BGMPlayer bgm = GetComponentInChildren<BGMPlayer>();
        if (bgm)
            bgm.PlaySound();

        _firstStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_subMenuOn && !_optionSubMenuOn)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OptionMenuOff();
            }
        }
    }
    public void OptionMenuOff()
    {
        //EventSystem.current.SetSelectedGameObject(null);
        _subMenuOn = false;
        _currentMenu.SetActive(false);
        //EventSystem.current.SetSelectedGameObject(null);
        // EventSystem.current.SetSelectedGameObject(_graghicsClosedButton);
        _prevUiButton.GetComponent<Button>().OnSelect(new BaseEventData(EventSystem.current));
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_prevUiButton);
    }
}
