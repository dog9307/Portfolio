using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndergroundChainSignal : MonoBehaviour
{
    public void DissolveEnd()
    {
        UndergroundChainController con = FindObjectOfType<UndergroundChainController>();
        if (con)
            con.DissolveEnd();
    }
}
