using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TodoList : MonoBehaviour
{
    [TextArea(10, 40)]
    [SerializeField]
    private string _memo;
}
