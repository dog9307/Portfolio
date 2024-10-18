using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SoundOption : MonoBehaviour
{
    public Slider[] soundBars;
    public Button[] buttons;
    public float[] Volumes;
    public int selectNum;
    public void Start()
    {

        selectNum = 0;
        buttons = GetComponentsInChildren<Button>();
        soundBars = GetComponentsInChildren<Slider>();

        EventSystem.current.SetSelectedGameObject(buttons[selectNum].gameObject);

        Volumes[0] = 1.0f;
        Volumes[1] = 1.0f;
        Volumes[2] = 1.0f;

    }
    public void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            soundBars[i].maxValue = Volumes[i];
        }
        soundBars[0].value = DataManager.instance.dataInfosfloat["Bgm사운드"];
        soundBars[1].value = DataManager.instance.dataInfosfloat["Effect사운드"];
        soundBars[2].value = DataManager.instance.dataInfosfloat["Voice사운드"];

        KeyBoardAction();

    }
    public void KeyBoardAction()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectNum = (selectNum + 1) % buttons.Length;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectNum = (selectNum - 1) % buttons.Length;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (selectNum < 3) this.BroadcastMessage(buttons[selectNum].name);
        }

    }
    public void BgmSound()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && DataManager.instance.dataInfosfloat["Bgm사운드"] > 0)
        {
            DataManager.instance.dataInfosfloat["Bgm사운드"] -= 0.1f;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && DataManager.instance.dataInfosfloat["Bgm사운드"] < 1)
        {
            DataManager.instance.dataInfosfloat["Bgm사운드"] += 0.1f;
        }
    }
    public void EffectSound()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && DataManager.instance.dataInfosfloat["Effect사운드"] > 0)
        {
            DataManager.instance.dataInfosfloat["Effect사운드"] -= 0.1f;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && DataManager.instance.dataInfosfloat["Effect사운드"] < 1)
        {
            DataManager.instance.dataInfosfloat["Effect사운드"] += 0.1f; ;
        }
    }
    public void VoiceSound()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && DataManager.instance.dataInfosfloat["Voice사운드"] > 0)
        {
            DataManager.instance.dataInfosfloat["Voice사운드"] -= 0.1f;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && DataManager.instance.dataInfosfloat["Voice사운드"] < 1)
        {
            DataManager.instance.dataInfosfloat["Voice사운드"] += 0.1f;
        }
    }
    public void ResetOption()
    {
        DataManager.instance.dataInfosfloat["Bgm사운드"] = 1.0F;
        DataManager.instance.dataInfosfloat["Effect사운드"] = 1.0F;
        DataManager.instance.dataInfosfloat["Voice사운드"] = 1.0F;
    }
    
}
