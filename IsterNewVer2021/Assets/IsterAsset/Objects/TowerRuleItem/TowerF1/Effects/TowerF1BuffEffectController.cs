using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerF1BuffEffectController : MonoBehaviour
{
    [SerializeField]
    private GameObject _normalEffect;
    [SerializeField]
    private ParticleSystem _destroyEffect;

    public FlowerType type { get; set; }

    private static TowerGardenManager _manager;

    void Start()
    {
        if (!_manager)
            _manager = FindObjectOfType<TowerGardenManager>();

        _manager.AddBuffEffect(this);
    }

    public void Destroy()
    {
        _normalEffect.SetActive(false);

        _destroyEffect.gameObject.SetActive(true);
    }

    public void MoveStart(Transform targetPos)
    {
        Vector3 localPos = transform.localPosition;
        transform.parent = targetPos;
        StartCoroutine(Move(localPos));
    }

    IEnumerator Move(Vector3 toLocalPos)
    {
        Vector3 fromLocalPos = transform.localPosition;
        float currentTime = 0.0f;
        float originTime = 4.0f;
        float timeRange = 0.5f;
        float totalTime = Random.Range(originTime - timeRange, originTime + timeRange);
        while (currentTime < totalTime)
        {
            float ratio = currentTime / totalTime;
            Vector3 newPos = Vector3.Lerp(fromLocalPos, toLocalPos, ratio);

            transform.localPosition = newPos;

            yield return null;

            currentTime += IsterTimeManager.enemyDeltaTime;
        }
    }
}
