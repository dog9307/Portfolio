using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueData : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private string _csv_FileName;
    public string csvName { get { return _csv_FileName; } set { _csv_FileName = value; } }

    private Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();

    public bool isFinish = false;

    public void Init(ref Vector2Int line)
    {
        if (_csv_FileName == "") return;

        Dialogue[] dialogues = DialogueParser.instance.Parse(_csv_FileName); // dialogue에 값이 다 들어감.

        //대사 인덱스 0부터 찾으면 직관적이지 않다.
        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogueDic.Add(i + 1, dialogues[i]);
        }
        line.x = 1;
        line.y = dialogueDic.Count;

        isFinish = true;
    }

    public Dialogue[] GetDialogues(int startNum, int endNum)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        //첫재줄과 마지막줄 사이에 있는거 꺼낼거니까. ex 1~3번째 줄이면 3-1 = 2 니까 3번 반복.
        for (int i = 0; i <= endNum - startNum; i++)
        {
            //위에 스타트 넘버부터 돌려줘야 하니까 
            dialogueList.Add(dialogueDic[startNum + i]);
        }
        //해당 다얄로그 반환.
        return dialogueList.ToArray();
    }
}
