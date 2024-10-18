using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerF1TutoManager : MonoBehaviour
{
    [SerializeField]
    private List<TowerF1FlowerGuide> _guides;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void FlowerGainSignal(FlowerType type)
    {
        foreach (var g in _guides)
        {
            if (g.type == type)
            {
                g.ShowGuide();
                break;
            }
        }
    }
}
