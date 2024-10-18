using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertSignal : MonoBehaviour
{
    [SerializeField]
    private string _alertDesc;
    public string alertDesc { get { return _alertDesc; } set { _alertDesc = value; } }

    public void Signal()
    {
        AlertController.instance.AlertIn(alertDesc);
    }
}
