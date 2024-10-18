using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingIllustSender : MonoBehaviour
{
    public void SendEndingKeyward(string keyward)
    {
        PlayerPrefs.SetString("EndingIllust", keyward);

        string endingKey = "Ending_" + keyward;
        PlayerPrefs.SetInt(endingKey, 100);
    }
}
