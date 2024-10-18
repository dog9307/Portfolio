using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuestElementController : MonoBehaviour
{
    [SerializeField]
    private int _questId;
    public int questId { get { return _questId; } }

    [SerializeField]
    private Image _line;
    [SerializeField]
    private Text _text;

    private int _currentFlowerCount = 0;

    public bool isAlreadyClear { get; set; }

    public UnityEvent OnQuestUpdate;
    public UnityEvent OnQuestClear;

    [SerializeField]
    private QuestListUIToggleController _ui;

    [SerializeField]
    private SFXPlayer _sfx;

    void Start()
    {
        _currentFlowerCount = 0;

        _line.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        int count = PlayerPrefs.GetInt($"quest_{_questId}", 0);
        if (count >= 100)
            QuestClear();
    }

    public void UpdateUI()
    {
        if (!isAlreadyClear)
        {
            if (OnQuestUpdate != null)
                OnQuestUpdate.Invoke();
        }

        if (_questId == 200)
        {
            if (_currentFlowerCount < 4)
            {
                _currentFlowerCount++;
                if (_currentFlowerCount >= 4)
                    QuestClear();

                _text.text = $"◆ 꽃 획득 - 체력 1칸 회복 ({_currentFlowerCount} / 4)";
            }
        }
        else if (_questId == 101)
        {
            if (_currentFlowerCount < 4)
            {
                _currentFlowerCount++;
                if (_currentFlowerCount >= 4)
                    QuestClear();
            }
        }
        else
            QuestClear();

        _ui.QuestListUpdate();
    }

    public void QuestClear()
    {
        if (isAlreadyClear) return;

        if (OnQuestClear != null)
            OnQuestClear.Invoke();

        isAlreadyClear = true;

        if (_sfx)
            _sfx.PlaySFX("clear");

        float colorValue = 0.5f;

        Color start = _text.color;
        Color end = new Color(colorValue, colorValue, colorValue, 1.0f);
        _text.color = end;

        _line.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        //_line.StartFading(1.0f);
        //StartCoroutine(ColorChange());
    }

    IEnumerator ColorChange()
    {
        float colorValue = 0.8f;
        float currentTime = 0.0f;
        float totalTime = 1.0f;
        Color start = _text.color;
        Color end = new Color(colorValue, colorValue, colorValue, 1.0f);
        while (currentTime < totalTime)
        {
            float ratio = currentTime / totalTime;
            _text.color = Color.Lerp(start, end, ratio);

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }
        _text.color = Color.Lerp(start, end, 1.0f);
    }
}
