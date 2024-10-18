using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorBulletController : MonoBehaviour
{
    public float _speed;
    public float _durationTime;
    public Vector3 _dir;

    private void OnEnable()
    {
        StartCoroutine(Move());
        Invoke("DestroyBullet", _durationTime);
    }
    
    IEnumerator Move()
    {
        // Vector3 newPos = transform.position;
        //
        // newPos.y -= _speed * IsterTimeManager.bossDeltaTime;

        while (true)
        {
            transform.position += _dir * _speed * IsterTimeManager.bossDeltaTime;

            yield return null;
        }
    }
    public void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}
