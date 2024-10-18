using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherSpirit : BossBase
{
    [SerializeField]
    private ParticleSystem[] _effects;

    [SerializeField]
    private ParticleSystem _shield;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        _isWaveBoss = true;
        _isPlayerCheck = true;
        
        //ParticleSystem.MainModule main = _shield.main;
        //ApplyAlpha(main, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void EffectOn()
    {
        foreach (var e in _effects)
            e.Play();
        
        _shield.Play();

       // StartCoroutine(ShieldFading(0.0f, 0.7f));
    }

   // IEnumerator ShieldFading(float from, float to)
   // {
   //     ParticleSystem.MainModule main = _shield.main;
   //
   //     float totalTime = 0.8f;
   //     float currentTime = 0.0f;
   //     while (currentTime < totalTime)
   //     {
   //         float ratio = currentTime / totalTime;
   //         float alpha = Mathf.Lerp(from, to, ratio);
   //
   //         ApplyAlpha(main, alpha);
   //
   //         yield return null;
   //
   //         currentTime += IsterTimeManager.bossDeltaTime;
   //     }
   // }
   //
   // void ApplyAlpha(ParticleSystem.MainModule main, float a)
   // {
   //     Color color = main.startColor.color;
   //     color.a = a;
   //     main.startColor = new ParticleSystem.MinMaxGradient(color);
   // }

    public void EffectOff()
    {
        foreach (var e in _effects)
            e.Stop();

        _shield.Stop();

      //  ParticleSystem.MainModule main = _shield.main;
      //  ApplyAlpha(main, 0.0f);
    }
}
