using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    static private DialogueParser _instance;
    static public DialogueParser instance { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<DialogueParser>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "DialogueParser";
                _instance = container.AddComponent<DialogueParser>();
            }
        }

        if (_instance != this)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(_instance);
    }

    public Dialogue[] Parse(string _CSVFileName)
    {
        List<Dialogue> dialogueList = new List<Dialogue>(); //대사 리스트
        string path = "DialogueData/" + _CSVFileName;
        TextAsset csvData = Resources.Load<TextAsset>(path); //csv파일 가져옴.

        string[] data = csvData.text.Split(new char[] {'\n'});//시트에서 작성한 ID 순서로 들어감
        
        for(int i = 1; i <data.Length;)
        {
            string[] row = data[i].Split(new char[] { ',' });

            Dialogue dialogue = new Dialogue();

            dialogue.id = int.Parse(row[0]);
            dialogue.name = row[1];
            List<string> contextList = new List<string>();

            do
            {
                contextList.Add(row[2]);
                if (++i < data.Length){
                    row = data[i].Split(new char[] { ',' });
                }
                else{
                    break;
                }
            } while (row[0].ToString() == "");

            dialogue.contexts = contextList.ToArray();
            dialogueList.Add(dialogue);

        }

        return dialogueList.ToArray();
    }
}
