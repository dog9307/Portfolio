using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GraphicsUI : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _buttons;

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))//||Input.GetButtonDown
        {

        }
    }
}
