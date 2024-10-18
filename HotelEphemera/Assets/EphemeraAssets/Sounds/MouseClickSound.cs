using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickSound : MonoBehaviour
{
    #region SINGLETON
    static private MouseClickSound _instance;
    static public MouseClickSound instance { get { return _instance; } }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (!_instance)
        {
            _instance = FindObjectOfType<MouseClickSound>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "RoomManager";
                _instance = container.AddComponent<MouseClickSound>();
            }
        }
        else
            Destroy(gameObject);
    }
    #endregion

    public AudioSource _audio;
    [Range(0, 1)]public float volume;
    // Update is called once per frame

    void Update()
    {
        _audio.volume =volume;
        if (Input.GetMouseButtonDown(0)) _audio.Play();
    }
}
