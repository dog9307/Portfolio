using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointerParticle : MonoBehaviour
{
    #region SINGLETON
    static private MousePointerParticle _instance;
    static public MousePointerParticle instance { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<MousePointerParticle>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "MousePointerParticle";
                _instance = container.AddComponent<MousePointerParticle>();
            }
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(MousePointerParticle.instance);
    }
    #endregion
}
