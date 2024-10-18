using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    #region SINGLETON
    static private TouchManager _instance;
    static public TouchManager instance { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<TouchManager>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "TouchManager";
                _instance = container.AddComponent<TouchManager>();
            }

            DontDestroyOnLoad(TouchManager.instance);
            _isEnable = true;
        }
        else
            Destroy(gameObject);
    }
    #endregion

    private bool _isEnable = true;
    public bool isEnable { get { return _isEnable; } set { _isEnable = value; } }

    public bool IsSingleTabDown()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
                return true;
        }

        return false;
    }

    public bool IsSingleTabUp()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
                return true;
        }

        return false;
    }
}
