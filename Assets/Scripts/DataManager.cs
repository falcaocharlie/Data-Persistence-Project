using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEditor;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string PlayerName;
    public int PlayerBestScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class PlayerData
    {
        public string PlayerName;
        public int PlayerBestScore;
    }

    public void SavePlayerData()
    {
        PlayerData data = new PlayerData();
        data.PlayerName = PlayerName;
        data.PlayerBestScore = PlayerBestScore;

        string json = JsonUtility.ToJson(data);

        string path = Application.persistentDataPath + "/savefile_" + PlayerName + ".json";
        File.WriteAllText(path, json);
    }

    public void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/savefile_" + PlayerName +".json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            PlayerName = data.PlayerName;
            PlayerBestScore = data.PlayerBestScore;
        }
    }
}


