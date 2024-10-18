using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFXChanger : MonoBehaviour
{
    [SerializeField]
    private string _sfxName;
    [SerializeField]
    private AudioClip _clip;
    [SerializeField]
    [Range(0.0f, 1.0f)] private float _volume = 1.0f;

    // Start is called before the first frame update
    public void ChangeSfx()
    {
        PlayerMoveController player = FindObjectOfType<PlayerMoveController>();
        if (player)
        {
            SFXPlayer sfx = player.GetComponentInChildren<SFXPlayer>();
            if (sfx)
                sfx.ChangeSFX(_sfxName, _clip, _volume);
        }
    }
}
