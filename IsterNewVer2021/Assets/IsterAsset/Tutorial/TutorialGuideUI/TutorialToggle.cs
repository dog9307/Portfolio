using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialToggle : MonoBehaviour
{
    [SerializeField]
    private FadingGuideUI _fading;
    [SerializeField]
    private TutorialFadingKeyTrigger _key;
    [SerializeField]
    private TutorialPlayerFreezing _freeze;

    // Start is called before the first frame update
    public void StartToggle()
    {
        _fading.StartFading(1.0f, true);
        _key.isEnable = true;
        _freeze.Freezing();
    }

    public void Toggle()
    {
        FindObjectOfType<QuestListUIToggleController>().Toggle();
    }
}
