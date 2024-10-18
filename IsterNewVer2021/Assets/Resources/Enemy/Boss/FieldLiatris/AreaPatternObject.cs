using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPatternObject : MonoBehaviour
{
    public ParticleSystem _particle;
    private void OnEnable()
    {
        _particle.Play();
    }
    private void OnDisable()
    {
        _particle.Stop();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("PLAYER"))
        {
            collision.gameObject.GetComponent<PlayerMoveController>().isHide = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("PLAYER"))
        {
            collision.gameObject.GetComponent<PlayerMoveController>().isHide = false;
        }

    }
}
