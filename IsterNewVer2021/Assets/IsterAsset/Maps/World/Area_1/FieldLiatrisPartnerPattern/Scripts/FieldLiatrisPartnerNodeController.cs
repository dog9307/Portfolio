using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldLiatrisPartnerNodeController : MonoBehaviour
{
    public FieldLiatrisPartnerManager manager { get; set; }

    [HideInInspector]
    [SerializeField]
    private SpriteRenderer _image;

    void Start()
    {
        _image.gameObject.SetActive(false);
    }

    public void TurnOnNode(Sprite sprite)
    {
        _image.gameObject.SetActive(true);

        _image.flipX = (transform.position.x > manager.transform.position.x);

        if (sprite)
            _image.sprite = sprite;
    }

    public void TurnOffNode()
    {
        _image.gameObject.SetActive(false);
    }
}
