using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArthasMenethil : MonoBehaviour
{
    private void OnDestroy()
    {
        if (transform.parent)
            Destroy(transform.parent.gameObject);
    }
}
