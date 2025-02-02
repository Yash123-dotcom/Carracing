using UnityEngine;
using TMPro;

public class Mainmenuhighscore : MonoBehaviour
{
    public TMP_Text HighScoreText;

    void Start()
    {
        LoadHighScore();
    }

    void LoadHighScore()
    {
        float bestTime = PlayerPrefs.GetFloat("BestSurvivalTime", 0); // Ensure the key matches SaveHighScore()
        HighScoreText.text = "Best Survival Time: " + bestTime.ToString("F2") + "s";
    }
}