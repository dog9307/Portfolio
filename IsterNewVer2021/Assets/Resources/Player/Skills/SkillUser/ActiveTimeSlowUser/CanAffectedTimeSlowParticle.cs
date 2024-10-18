using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanAffectedTimeSlowParticle : MonoBehaviour
{
    public struct ParticleTimeNode
    {
        public ParticleSystem owner;
        public float startSimulationSpeed;
    }

    [SerializeField]
    private ParticleSystem[] _effects;

    private List<ParticleTimeNode> _nodes = new List<ParticleTimeNode>();

    [SerializeField]
    private bool _isBoss = false;
    public bool isBoss { get { return _isBoss; } set { _isBoss = value; } }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var e in _effects)
        {
            ParticleTimeNode node = new ParticleTimeNode();
            node.owner = e;
            node.startSimulationSpeed = e.main.simulationSpeed;

            _nodes.Add(node);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float timeMultiplier = 1.0f;

        if (_isBoss)
            timeMultiplier = IsterTimeManager.bossTimeScale;
        else
            timeMultiplier = IsterTimeManager.enemyTimeScale;

        foreach (var p in _nodes)
        {
            if (!p.owner) continue;

            ParticleSystem.MainModule main = p.owner.main;
            main.simulationSpeed = p.startSimulationSpeed * timeMultiplier;
        }
    }
}
