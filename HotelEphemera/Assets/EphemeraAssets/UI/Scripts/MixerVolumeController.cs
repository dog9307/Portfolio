using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MixerVolumeController : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private Slider _slider;
    [HideInInspector]
    [SerializeField]
    private AudioMixer _mixer;
    [SerializeField]
    private string _paramName;

    [SerializeField]
    private float _minVol = -40.0f;
    [SerializeField]
    private float _maxVol = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //_mixer.SetFloat(_paramName, )
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUp()
    {
        print("mouseup");
    }
}
