using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCsvParser : MonoBehaviour
{
    static private DialogueCsvParser _instance;
    static public DialogueCsvParser instance { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<DialogueCsvParser>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "DialogueCsvParser";
                _instance = container.AddComponent<DialogueCsvParser>();
            }
        }
    }

    public Dialogue[] Parse(string _CSVFileName)
    {
        List<Dialogue> dialogueList = new List<Dialogue>(); //대사 리스트 생성 
        string path = "DialogueData/" + _CSVFileName;
        TextAsset csvData = Resources.Load<TextAsset>(path); //csv파일 가져옴.

        string[] data = csvData.text.Split(new char[] { '\n' });//시트에서 작성한 줄 갯수.

        //0번째 인 첫번 째 줄은 정보에 대한 내용만 있으니 그 다음인 1번부터 마지막 줄 까지.
        for (int i = 1; i < data.Length;)
        {
            //해당 줄 을 다 쪼개줌. , 기준 (id , 이름 , 대화 내용등)
            string[] row = data[i].Split(new char[] { ',' });

            Dialogue dialogue = new Dialogue();

            dialogue.name = row[1];

            //대화 내용 여러줄을 넣기 위해 선언.
            List<string> contextList = new List<string>();

            //같은 사람이 대화를 하면 여백이 생기므로 다음 row가 여백일 시 조건을 돌림.
            do
            {
                //말하는 사람이 같으니 대화만 추가.
                contextList.Add(row[2]);
                //다음 줄이 있다면.
                if (++i < data.Length)
                {
                    //해당 줄의 내용 쪼개서 넣어줌.
                    row = data[i].Split(new char[] { ',' });
                }
                else
                {
                    //없으면 내용 끝.
                    break;
                }
            } while (row[0].ToString() == ""); //id 가 바뀔 때. 

            //저장 해놓은 대화 내용을 다얄로그 컨택스트에 넣어줌.
            dialogue.contexts = contextList.ToArray();

            //다얄로그 리스트에 다얄로그를 넣음.
            dialogueList.Add(dialogue);

        }

        //파서에서 바로 다얄로그 리스트를 반환해줌.
        return dialogueList.ToArray();
    }
}

