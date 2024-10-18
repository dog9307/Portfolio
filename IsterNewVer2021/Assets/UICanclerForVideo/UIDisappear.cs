using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDisappear : MonoBehaviour
{
    [SerializeField]
    private GameObject _developPack;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            Cursor.visible = true;
            foreach (var c in FindObjectsOfType<Canvas>())
                c.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.F10))
        {
            Cursor.visible = false;

            Canvas[] dialogueCanvas = DialogueManager.instance.GetComponentsInChildren<Canvas>();
            foreach (var c in FindObjectsOfType<Canvas>())
            {
                bool isContinue = false;
                for (int i = 0; i < dialogueCanvas.Length; ++i)
                {
                    if (dialogueCanvas[i] == c)
                    {
                        isContinue = true;
                        break;
                    }
                }
                if (isContinue) continue;

                c.enabled = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.F11))
        {
            PlayerMoveController player = FindObjectOfType<PlayerMoveController>();

            Renderer[] renderers = player.GetComponentsInChildren<Renderer>(true);
            foreach (var r in renderers)
                r.enabled = !r.enabled;

            Collider2D[] cols = player.GetComponentsInChildren<Collider2D>(true);
            foreach (var c in cols)
                c.enabled = !c.enabled;
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            if (_developPack)
            {
                GameObject pack = Instantiate(_developPack);
                pack.transform.position = FindObjectOfType<PlayerMoveController>().transform.position;
            }
        }
    }
}
