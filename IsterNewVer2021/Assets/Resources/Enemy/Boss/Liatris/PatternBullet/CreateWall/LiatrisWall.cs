using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiatrisWall : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _walls = new List<GameObject>();

    [SerializeField]
    ParticleSystem _wallEffect;

    public float _keepTime;
    float _currentTime;
    [SerializeField]
    SFXPlayer _sfx;
    private void OnEnable()
    {
        _currentTime = 0;

        if (_wallEffect)
            _wallEffect.Play();
        if (_sfx) _sfx.PlaySFX("wall_create");
    }


    // Update is called once per frame
    void Update()
    {
        if (_keepTime > _currentTime)
        {
            _currentTime += IsterTimeManager.bossDeltaTime;
        }
        else DestroyWall();
    }
    void DestroyWall()
    {
        if (_sfx) _sfx.PlaySFX("wall_destroy");
        
        for (int i = _walls.Count - 1; i >= 0; i --)
        {
            if (_walls[i].gameObject)
            {
                GameObject obj = _walls[i].gameObject;
               // obj.GetComponent<SafetyZoneTrigger>().SafetyZoneOff();
                Destroy(obj);
                _walls.RemoveAt(i);
            }
            else continue;
        }
    }
}
