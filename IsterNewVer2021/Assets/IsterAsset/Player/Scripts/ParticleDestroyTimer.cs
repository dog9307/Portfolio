using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyTimer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float max = float.MinValue;
        ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
        foreach(var particle in particles)
        {
            if (max < particle.main.duration)
                max = particle.main.duration;
        }

        print(max);

        Invoke("Destroy", max);
        Invoke("ColliderDestroy", 0.1f);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    void ColliderDestroy()
    {
        Collider2D[] cols = GetComponentsInChildren<Collider2D>();
        foreach (var col in cols)
            col.enabled = false;
    }
}
