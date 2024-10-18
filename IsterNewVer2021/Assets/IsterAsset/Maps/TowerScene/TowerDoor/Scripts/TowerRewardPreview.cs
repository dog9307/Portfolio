using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRewardPreview : MonoBehaviour
{
    [SerializeField]
    private GameObject _guideArrow;

    void Update()
    {
        bool isInScreen = false;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        isInScreen = (0.0f < screenPos.x && screenPos.x < Screen.width) &&
                     (0.0f < screenPos.y && screenPos.y < Screen.height);

        _guideArrow.SetActive(!isInScreen);
    }
}
