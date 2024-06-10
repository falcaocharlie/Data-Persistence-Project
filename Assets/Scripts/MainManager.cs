using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private string m_PlayerName = string.Empty;
    private int m_Points;
    private int m_BestPoints;

    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        print("uno");
        m_PlayerName = DataManager.Instance.PlayerName;
        m_BestPoints = DataManager.Instance.PlayerBestScore;
        BestScoreText.text = $"BestScore : " + m_PlayerName + " : " + m_BestPoints.ToString();

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            print("tre");
            m_PlayerName = DataManager.Instance.PlayerName;
            m_BestPoints = DataManager.Instance.PlayerBestScore;
            BestScoreText.text = $"BestScore : " + m_PlayerName + " : " + m_BestPoints.ToString();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            print("due");
            DataManager.Instance.PlayerName = m_PlayerName;
            DataManager.Instance.PlayerBestScore = m_BestPoints;
            DataManager.Instance.SavePlayerData();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        if(m_BestPoints < m_Points) m_BestPoints = m_Points;
        BestScoreText.text = $"BestScore : " + m_PlayerName + " : " + m_BestPoints.ToString();
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
