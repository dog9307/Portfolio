using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMarkerController : MonoBehaviour, IObjectCreator
{
    [SerializeField]
    [Range(0.1f, 3.0f)] private float _tickTime;
    public float tickTime
    {
        get
        {
            if (!user)
                return _tickTime;

            float tick = _tickTime - _tickTime * user.tickMultiplier;

            return tick;
        }
    }
    
    public GameObject effectPrefab { get; set; }

    public ActiveMarkerUser user { get; set; }

    void Start()
    {
        EventTimer destroyTimer = GetComponent<EventTimer>();
        destroyTimer.totalTime += user.additionalTime;
        destroyTimer.AddEvent(this.Destroy);

        effectPrefab = Resources.Load<GameObject>("Player/Bullets/ActiveMarker/Prefab/MarkingEffect");
        
        CreateMarkingEffect();
    }

    [SerializeField]
    private ParticleSystem _effect;
    public void Destroy()
    {
        _effect.Stop();
    }

    public void CreateMarkingEffect()
    {
        GameObject markingEffect = CreateObject();
        Marking marking = markingEffect.GetComponent<Marking>();
        marking.user = user;

        EventTimer timer = gameObject.AddComponent<EventTimer>();
        timer.totalTime = tickTime;
        timer.AddEvent(CreateMarkingEffect);
        timer.AddEvent(timer.DestroyTimer);
    }

    public GameObject CreateObject()
    {
        GameObject newMarking = Instantiate(effectPrefab);
        newMarking.transform.parent = transform;
        newMarking.transform.localPosition = Vector3.zero;

        return newMarking;
    }
}
