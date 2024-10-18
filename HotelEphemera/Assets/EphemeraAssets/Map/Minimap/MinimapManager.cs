using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    #region SINGLETON
    static private MinimapManager _instance;
    static public MinimapManager instance { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<MinimapManager>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "MinimapManager";
                _instance = container.AddComponent<MinimapManager>();
            }
        }
        else
            Destroy(gameObject);
    }
    #endregion

    [SerializeField]
    private GameObject[] _minimaps;

    private GameObject _currentMinimap;
    public GameObject currentMinimap { get { return _currentMinimap; } }

    private ImageSpriteChanger[] _changers;

    void Start()
    {
        // test
        _currentMinimap = _minimaps[0];
        ImageChange(0);
    }

    void ImageChange(int index)
    {
        if (_changers == null)
            _changers = FindObjectsOfType<ImageSpriteChanger>(true);

        if (_changers != null)
        {
            foreach (var c in _changers)
                c.SpriteChange(index);
        }
    }

    private MinimapSetter _setter;

    public void OpenMinimap(int index)
    {
        if (index == 0)
        {
            int check = RoomRenderer.instance.roomNumber / 10;
            if (check == 1)
                index = check;
        }

        _currentMinimap?.SetActive(false);

        _currentMinimap = _minimaps[index];
        _currentMinimap?.SetActive(true);

        if (!_setter)
            _setter = FindObjectOfType<MinimapSetter>();

        if (_setter)
            _setter.currentMinimap = _currentMinimap.GetComponent<MinimapController>();

        ImageChange(index);
    }
}
