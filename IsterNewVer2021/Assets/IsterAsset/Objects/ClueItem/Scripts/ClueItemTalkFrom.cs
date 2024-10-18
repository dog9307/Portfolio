using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClueItemTalkFrom : TalkFrom
{
    [SerializeField]
    private string _keyward = "";
    [SerializeField]
    private int _id = 100;

    [HideInInspector]
    [SerializeField]
    private RelicDisposableObject _disposable;

    //[HideInInspector]
    //[SerializeField]
    //private SavableItem _save;

    public UnityEvent OnTalk;

    [SerializeField]
    private bool _isDestroy = true;

    public override void Talk()
    {
        PlaySFX();

        SavableNode node = null;

        node = new SavableNode();
        node.key = $"Clue_{_keyward}";
        node.value = 100;
        SavableDataManager.instance.AddSavableObject(node);

        node = new SavableNode();
        node.key = $"Clue_{_keyward}_{_id}";
        node.value = 100;
        SavableDataManager.instance.AddSavableObject(node);

        ClueSlotList clueSlotList = FindObjectOfType<ClueSlotList>();
        if (clueSlotList)
            clueSlotList.ButtonOn(_keyward, _id);

        if (_disposable)
            _disposable.UseObject();

        if (OnTalk != null)
            OnTalk.Invoke();

        if (_isDestroy)
            Destroy(gameObject);
    }
}
