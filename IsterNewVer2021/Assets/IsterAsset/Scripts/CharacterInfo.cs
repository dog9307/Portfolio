using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    protected string _characterName;
    public string characterName { get { return _characterName; } set { _characterName = value; } }

    [SerializeField] protected RuntimeAnimatorController _controller;
    public RuntimeAnimatorController animController { get { return _controller; } }

    void Awake()
    {
        ChangeName();
    }
    
    public void ChangeName()
    {
        if (_controller)
        {
            int index = animController.name.IndexOf('_');
            _characterName = _controller.name.Substring(0, index);
        }
    }
}
