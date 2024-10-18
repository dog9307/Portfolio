using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWallBullet : MonoBehaviour , IObjectCreator
{
    PlayerMoveController _player;

    [SerializeField]
    ParticleSystem _tail;

    [SerializeField]
    GameObject _wallObject;
    public GameObject effectPrefab { get { return _wallObject; } set { _wallObject = value; } }

    Vector3 _dir;

    public float _bulletSpeed;
    // Start is called before the first frame update
    private void OnEnable()
    {
       _player = FindObjectOfType<PlayerMoveController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_player)
        {
            if (collision.tag.Equals("PLAYER"))
            {
                CreateObject();
                Destroy(this.gameObject);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

        var targetDir = (_player.center - transform.position).normalized;
        Vector3 newPos = transform.position;
        transform.position = Vector3.Slerp(newPos, _player.center, _bulletSpeed * IsterTimeManager.bossDeltaTime);

        if (_tail)
            _tail.transform.position = transform.position;

    }
    void Createwall()
    {
        if(_wallObject)
            _wallObject.SetActive(true);

        Destroy(this.gameObject);
    }
    public GameObject CreateObject()
    {
        GameObject newBullet = Instantiate<GameObject>(effectPrefab);
        newBullet.transform.position = _player.center;
        return newBullet;
    }

}
