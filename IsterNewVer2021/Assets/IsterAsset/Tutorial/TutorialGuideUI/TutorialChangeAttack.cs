using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialChangeAttack : MonoBehaviour
{
    void Start()
    {
        GetComponent<FadingGuideUI>().StartFading(1.0f);
        GetComponent<TutorialFadingKeyTrigger>().isEnable = true;
    }

    public void ChangeAttack()
    {
        PlayerPassiveSkillStorage storage = FindObjectOfType<PlayerPassiveSkillStorage>();
        if (storage)
        {
            //storage.currentPassiveIndex = (storage.currentPassiveIndex + 1) % storage.passiveSkills.Count;
            //storage.ChangePassive();
        }
    }
}
