using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Calcatz.ArrivalGUI;

public class InGameUIFinder : MonoBehaviour
{
    [SerializeField]
    private FadingGuideUI _skillPanel;
    [SerializeField]
    private FadingGuideUI _darklightPanel;

    private PlayerInventory _playerInventory;

    public bool isDialogueOn { get; set; }

    //public void StartFading()
    //{
    //    StartCoroutine(Fading());
    //}

    //IEnumerator Fading()
    //{
    //    float currentTime = 0.0f;
    //    float totalTime = 0.0f;
    //    while (currentTime < totalTime)
    //    {
    //        float ratio = currentTime / totalTime;
    //        ApplyAlpha(1.0f - ratio);

    //        yield return null;
            
    //        currentTime += IsterTimeManager.originDeltaTime;
    //    }
    //    ApplyAlpha(0.0f);
    //}

    //void ApplyAlpha(float alpha)
    //{
    //    foreach (var g in _graphics)
    //    {
    //        Color color = g.color;
    //        color.a = alpha;
    //        g.color = color;
    //    }
    //}

    public void GraphicsOn()
    {
        _skillPanel.StartFading(1.0f);

        if (!_playerInventory)
            _playerInventory = FindObjectOfType<PlayerInventory>();

        if (_playerInventory.darkLight > 0)
            _darklightPanel.StartFading(1.0f);
    }
     
    public void GraphicsOff()
    {
        _skillPanel.StartFading(0.0f);

        if (!_playerInventory)
            _playerInventory = FindObjectOfType<PlayerInventory>();

        if (_playerInventory.darkLight > 0)
            _darklightPanel.StartFading(0.0f);
    }
}
