using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherSpiritGrogiEvent : MonoBehaviour
{
    private MotherSpiritController _mother;
    private bool _prevGrogi;

    void Start()
    {
        _mother = GetComponent<MotherSpiritController>();
    }

    void Update()
    {
        if (!_prevGrogi && _mother._grogi)
            _mother.SauronAllStop();

        _prevGrogi = _mother._grogi;
    }
}
