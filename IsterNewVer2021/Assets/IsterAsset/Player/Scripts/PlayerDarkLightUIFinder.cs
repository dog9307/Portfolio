using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDarkLightUIFinder : MonoBehaviour
{
    [SerializeField]
    private FadingGuideUI _fading;

    // Start is called before the first frame update
    void Start()
    {
        int count = PlayerPrefs.GetInt("PlayerDarkLightFirstGain", 0);
        if (count >= 100)
            _fading.isShow = true;
        else
            _fading.isShow = false;
    }

    public void GainDarkLight()
    {
        SavableNode save = new SavableNode();
        save.key = "PlayerDarkLightFirstGain";
        save.value = 100;

        SavableDataManager.instance.AddSavableObject(save);

        _fading.StartFading(1.0f);
    }
}
