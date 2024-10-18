using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UiBase : MonoBehaviour
{
    [SerializeField]
    protected Image _firstImage; //current
    [SerializeField]
    protected Image _SecondImage; //backgrund

    public abstract void Init();

    void Start()
    {
        Init();
    }

    void Update()
    {
        ActiveUI();
    }
    public abstract void ActiveUI();
    //ui타입
}
