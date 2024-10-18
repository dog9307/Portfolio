using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISelectHelper : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    private Button _button;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_button)
        {
            if (!_button.interactable)
                return;
        }

        EventSystem.current.SetSelectedGameObject(gameObject);
    }
}
