using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RaceTimer : MonoBehaviour
{
    public float survivalTime = 0f;
    private bool isAlive = true;
    public TMP_Text timerText;
    public TMP_Text highScoreText;

    void Start()
    {
        LoadHighScore();
    }

    void Update()
    {
        if (isAlive)
        {
            survivalTime += Time.deltaTime;
            UpdateTimerUI();
            IncreaseDifficulty();
        }
    }

    public void GameOver()
    {
        isAlive = false;
        Debug.Log("Game Over called! Saving High Score...");
        SaveHighScore();
    }

    void SaveHighScore()
    {
        float bestTime = PlayerPrefs.GetFloat("BestSurvivalTime", 0); // Make sure the key is consistent
        if (survivalTime > bestTime)
        {
            PlayerPrefs.SetFloat("BestSurvivalTime", survivalTime); // Correct key
            PlayerPrefs.Save();
        }
    }


    void LoadHighScore()
    {
        float bestTime = PlayerPrefs.GetFloat("BestSurvivalTime", 0);
        highScoreText.text = "Best: " + bestTime.ToString("F2") + "s";
    }

    void UpdateTimerUI()
    {
        float bestTime = PlayerPrefs.GetFloat("BestSurvivalTime", 0);
        timerText.text = "Time: " + survivalTime.ToString("F2") + "s\nBest: " + bestTime.ToString("F2") + "s";
    }

    public void stopsurvival()
    {
        isAlive = false;
    }

    void IncreaseDifficulty()
    {
        float difficultyMultiplier = 1f + (survivalTime / 30f); // Increases every 30s
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject obstacle in obstacles)
        {
            Rigidbody2D rb = obstacle.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity *= difficultyMultiplier;
            }
        }
    }
}
