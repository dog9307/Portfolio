using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreePanel : MonoBehaviour
{
    private string path = "UIs/SkillTreeSystem/SkillTree/Panels/";
    private List<GameObject> _panels = new List<GameObject>();

    private GameObject _currentPanel;

    private void OnDisable()
    {
        if (_currentPanel)
            _currentPanel.SetActive(false);

        _currentPanel = null;
    }

    public void ChangeTree(SkillBase skill)
    {
        if (skill == null) return;

        string name = skill.GetType().Name;
        GameObject panel = FindPanel(name);
        if (!panel)
        {
            GameObject prefab = Resources.Load<GameObject>(path + name);
            if (!prefab) return;

            panel = Instantiate(prefab);
            panel.name = name;
            panel.GetComponent<RectTransform>().parent = GetComponent<RectTransform>();
            _panels.Add(panel);
        }

        if (_currentPanel)
            _currentPanel.SetActive(false);

        _currentPanel = panel;

        RectTransform rect = _currentPanel.GetComponent<RectTransform>();
        rect.localPosition = Vector3.zero;
        rect.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        _currentPanel.SetActive(true);
    }

    public GameObject FindPanel(string name)
    {
        GameObject find = null;
        for (int i = 0; i < _panels.Count;)
        {
            if (!_panels[i])
            {
                _panels.Remove(_panels[i]);
                continue;
            }
            
            if (_panels[i].name == name)
            {
                find = _panels[i];
                break;
            }

            ++i;
        }

        return find;
    }
}
