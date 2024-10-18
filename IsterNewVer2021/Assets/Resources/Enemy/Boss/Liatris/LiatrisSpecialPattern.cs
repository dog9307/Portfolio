using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiatrisSpecialPattern : MonoBehaviour
{
    [SerializeField]
    BossController _controller;

   // [SerializeField]
   // GameObject _floor;

    [SerializeField]
    Damagable _damagable;
 
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    //public void StartPattern()
    //{
    //    _floor.gameObject.SetActive(true);
    //}
    //public void DisappearPattern()
    //{
    //    StartCoroutine(CoolTime());
    //}
    //
    //IEnumerator CoolTime()
    //{
    //    _floor.gameObject.SetActive(false);
    //
    //    float currentTime = 0.0f;
    //    while (currentTime < _controller._grogiResetTimer)
    //    {
    //        yield return null;
    //
    //        currentTime += IsterTimeManager.bossDeltaTime;
    //    }
    //    StartPattern();
    //}
}
