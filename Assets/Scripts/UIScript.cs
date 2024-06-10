using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(1000)]
public class UIScript : MonoBehaviour
{
    public TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Load()
    {
        string playerName = inputField.text;
        if (playerName.Length == 0) return;
        DataManager.Instance.PlayerName = playerName;
        DataManager.Instance.PlayerBestScore = 0;
        DataManager.Instance.LoadPlayerData();
        SceneManager.LoadScene(1);

    }
    public void Exit()
    {
        DataManager.Instance.SavePlayerData();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}
