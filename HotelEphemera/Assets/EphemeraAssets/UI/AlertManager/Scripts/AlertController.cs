using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Michsky.UI.Dark;

public class AlertController : MonoBehaviour
{
    #region SINGLETON
    static private AlertController _instance;
    static public AlertController instance { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<AlertController>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "AlertController";
                _instance = container.AddComponent<AlertController>();
            }
        }
        else
            Destroy(gameObject);
    }
    #endregion

    [HideInInspector]
    [SerializeField]
    private ModalWindowManager _modal;

    [HideInInspector]
    [SerializeField]
    private BlurManager _blur;

    [SerializeField]
    private CanvasGroup[] _groups;

    [SerializeField]
    private Button _backButton;

    public void AlertIn(string desc)
    {
        GroupsInteractable(false);

        _modal.description = desc;

        _modal.ModalWindowIn();
        _blur.BlurInAnim();
    }

    public void GroupsInteractable(bool interactable)
    {
        if (_groups != null)
        {
            foreach (var g in _groups)
                g.interactable = interactable;
        }

        if (_backButton)
            _backButton.interactable = interactable;
    }
}
