using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterLoader : MonoBehaviour
{
    private int _currentChapter;

    // Start is called before the first frame update
    void Start()
    {
        _currentChapter = PlayerPrefs.GetInt("LastPlayedChapter", 0);

        StringBuilder builder = new StringBuilder("Chapter" + _currentChapter);
        SceneManager.LoadSceneAsync(builder.ToString(), LoadSceneMode.Additive);
    }
}
