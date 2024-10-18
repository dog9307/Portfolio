using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IObjectCreator
{
    [SerializeField]
    private Transform _respawnPosition;

    [SerializeField]
    private Animator _magicCircle;
    [SerializeField]
    private Animator _effect;

    public GameObject enemy { get; set; }

    [SerializeField]
    private GameObject _effectPrefab;
    public GameObject effectPrefab { get { return _effectPrefab; } set { _effectPrefab = value; } }

    public delegate void SpawnEndEvent(GameObject obj);
    private Queue<SpawnEndEvent> _evt = new Queue<SpawnEndEvent>();

    private void Start()
    {
        CreateObject();
    }

    public void AddEvent(SpawnEndEvent evt)
    {
        _evt.Enqueue(evt);
    }

    [HideInInspector]
    public bool _nameSpawner;

    void OnDisable()
    {
        enemy = null;
    }

    public void SpawnEnemy()
    {
        if (!enemy) return;

        enemy.transform.position = _respawnPosition.position;
        enemy.SetActive(true);

        while (_evt.Count != 0)
        {
            SpawnEndEvent evt = _evt.Dequeue();
            evt(gameObject);
        }
    }

    public GameObject CreateObject()
    {
        GameObject effect = Instantiate(effectPrefab);
        effect.transform.position = transform.position - (_respawnPosition.position - transform.position);
        transform.parent = effect.transform;

        return effect;
    }
}
