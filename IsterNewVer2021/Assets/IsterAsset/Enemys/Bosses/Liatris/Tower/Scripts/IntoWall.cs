using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntoWall : MonoBehaviour
{
    [SerializeField]
    Collider2D _collider;

    [SerializeField]
    LiatrisController _liatrisCon;
    // Start is called before the first frame update
    void Start()
    {
        _liatrisCon = FindObjectOfType<LiatrisController>();
    }

    // Update is called once per frame

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("PLAYER") )
        {
            if (!_liatrisCon._isSword) collision.gameObject.GetComponent<PlayerMoveController>().isHide = true;
            else return;
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
