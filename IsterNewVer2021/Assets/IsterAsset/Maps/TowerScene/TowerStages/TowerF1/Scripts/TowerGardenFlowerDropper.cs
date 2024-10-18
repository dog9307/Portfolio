using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGardenFlowerDropper : MonoBehaviour, IObjectCreator
{
    [SerializeField]
    private GameObject _flowerPrefab;

    [SerializeField]
    private GameObject _target;

    [SerializeField]
    private Transform _flowerPos;
    [SerializeField]
    private FlowerType _type;
    [SerializeField]
    private Color _color;

    [SerializeField]
    private FieldDoorController _door;
    public FieldDoorController door { get { return _door; } }

    public GameObject effectPrefab { get => _flowerPrefab; set => _flowerPrefab = value; }

    [SerializeField]
    private ParticleSystem[] _effects;

    public GameObject CreateObject()
    {
        GameObject newFlower = Instantiate(_flowerPrefab);

        return newFlower;
    }

    // Update is called once per frame
    void Update()
    {
        if (_target) return;

        GameObject newFlower = CreateObject();

        if (newFlower)
        {
            newFlower.transform.position = _flowerPos.position;

            TowerF1FlowerTalkFrom talkFrom = newFlower.GetComponent<TowerF1FlowerTalkFrom>();
            if (talkFrom)
            {
                talkFrom.Init(_type, _color, null);
                talkFrom.dropper = this;
            }

            if (_effects != null)
            {
                foreach (var e in _effects)
                    e.Stop();
            }

            Destroy(this);
        }
    }
}
