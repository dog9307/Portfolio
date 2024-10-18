using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsResetter : MonoBehaviour
{
    [SerializeField]
    private Button _startButton;

    // Start is called before the first frame update
    void Start()
    {
        _startButton.interactable = (PlayerPrefs.GetInt("PlayerAlreadyStart", 0) >= 100);
        //ResetData();
    }

    // test
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.F1))
    //        _runner.StartDialogue("Illima_dialogue");
    //}

    public void ResetData()
    {
        //PlayerPrefs.SetInt("isPlayerDie", 0);
        //PlayerPrefs.SetInt("isPlayerAlreadyTalk", 0);
        //PlayerPrefs.SetInt("swordCount", 0);
        //PlayerPrefs.SetInt("DummyroomTutorial", 0);
        //PlayerPrefs.SetInt("LobbyTutorial", 0);
        //PlayerPrefs.SetInt("StartRoomTutorial", 0);
        //PlayerPrefs.SetInt("SkillChangeTutorial", 0);

        //PlayerPrefs.SetInt("UndergroundChainDone", 0);

        //for (int i = (int)ACTIVE.NONE + 1; i < (int)ACTIVE.END; ++i)
        //{
        //    int id = 100 + i;
        //    string key = "active_" + id.ToString();

        //    PlayerPrefs.SetInt(key, 0);

        //    id = 200 + i;
        //    key = "passive_" + id.ToString();

        //    PlayerPrefs.SetInt(key, 0);
        //}

        //PlayerPrefs.SetInt("relic_100", 0);

        //PlayerPrefs.SetInt("quest_100", 0);
        //PlayerPrefs.SetInt("quest_101", 0);
        //PlayerPrefs.SetInt("quest_102", 0);
        //PlayerPrefs.SetInt("quest_103", 0);
        //PlayerPrefs.SetInt("quest_200", 0);

        //PlayerPrefs.SetInt("questOver", 0);

        //for (int i = 0; i < 4; ++i)
        //{
        //    string key = "active_slot_" + i.ToString();
        //    PlayerPrefs.SetInt(key, 0);
        //}

        //PlayerPrefs.SetInt("UndergroundChainDone", 0);
        //PlayerPrefs.SetInt("PlayerNextStartPos", -1);
        //PlayerPrefs.SetInt("Darklight", 0);
        //PlayerPrefs.SetString("PlayerCurrentMap", "");

        //PlayerPrefs.SetInt("minimap_button_100", 0);
        //PlayerPrefs.SetInt("minimap_button_101", 0);
        //PlayerPrefs.SetInt("minimap_button_102", 0);
        //PlayerPrefs.SetInt("minimap_button_103", 0);
        //PlayerPrefs.SetInt("minimap_button_200", 0);
        //PlayerPrefs.SetInt("minimap_button_700", 0);

        PlayerPrefs.DeleteAll();
    }
}
