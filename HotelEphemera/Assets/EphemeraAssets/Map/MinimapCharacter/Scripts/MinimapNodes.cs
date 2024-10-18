using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapNodes : MonoBehaviour
{
    [SerializeField]
    private List<MinimapNodes> _connectedNodes = new List<MinimapNodes>();

    public void FindNodes(List<MinimapNodes> pathList, MinimapNodes prevNode, MinimapNodes target)
    {
        if (this == target)
            pathList.Add(this);
        else
        {
            int prevCount = pathList.Count;
            foreach (var n in _connectedNodes)
            {
                if (prevNode == n) continue;

                n.FindNodes(pathList, this, target);

                if (prevCount != pathList.Count)
                {
                    pathList.Add(this);
                    return;
                }
            }
        }
    }
}
