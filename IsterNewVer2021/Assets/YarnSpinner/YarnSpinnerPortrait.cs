using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnSpinnerPortrait : MonoBehaviour
{
    DialogueRunner dialogue;
    // Start is called before the first frame update
    void Start()
    {
        dialogue = GetComponentInChildren<DialogueRunner>();
        dialogue.startNode = "Test";
    }

    // Update is called once per frame
    void Update()
    {
        Input.GetKeyDown(KeyCode.F11);
        {
            dialogue.StartDialogue(dialogue.startNode);
        }
    }
}
