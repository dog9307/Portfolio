using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStartRoom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int dummyroomTutorial = PlayerPrefs.GetInt("StartRoomTutorial", 0);
        if (dummyroomTutorial != 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("StartRoomTutorial", 1);
    }
}
