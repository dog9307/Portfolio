using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagerOnOff : MonoBehaviour
{
    [SerializeField]
    private FloorTrap _floor;
    
    public void DamagerOn()
    {
        _floor._trapDamager.SetActive(true);
    }
    public void DamagerOff()
    {
        _floor._trapDamager.SetActive(false);
    }
}
