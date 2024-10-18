using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyZoneTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject _safetyZone;
    [SerializeField]
    ParticleSystem particle;

    private void OnEnable()
    {
        particle.Play();
    }
    // Start is called before the first frame update
    public void SafetyZoneOn()
    {
        _safetyZone.SetActive(true);
    }
    public void SafetyZoneOff()
    {
        _safetyZone.SetActive(false);
    }
}
