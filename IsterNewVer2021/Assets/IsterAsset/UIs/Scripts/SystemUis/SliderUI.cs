using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    Slider _slider;
    public Slider slider { get; }

    // Start is called before the first frame update
    void Start()
    {
        _slider = this.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
