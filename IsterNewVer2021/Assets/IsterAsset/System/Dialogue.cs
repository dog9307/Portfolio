using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct Dialogue
{
    [Tooltip("사람")]
    public string name;
    [Tooltip("내용")]
    public string[] contexts;
    [Tooltip("아이디")]
    public int id;
    [Tooltip("이미지")]
    public Sprite sprite;
    [Tooltip("방향")]
    public bool isLeft;
}


[System.Serializable]
public struct DialogueEvent
{
    public string name;
    [HideInInspector]
    public Vector2Int line;
    public Dialogue[] dialogues;

    public int startIndex { get { return line.x;} }
    public int endIndex { get { return line.y; } }
}

