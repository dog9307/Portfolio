using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TutorialDestroyTrigger : MonoBehaviour
{
    public bool isEnable { get; set; } = true;

    public UnityEvent OnTriggered;

    private void OnDestroy()
    {
        if (!isEnable) return;

        if (OnTriggered != null)
            OnTriggered.Invoke();
    }
}
