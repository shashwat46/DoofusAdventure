using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TextMeshProUGUI scoreText;
    public int pointsPerPulpit = 10;

    private int pulpitCount = 0;
    private int currentScore = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncrementPulpitCount()
    {
        pulpitCount++;
    }

    public void UpdateScore()
    {
        currentScore += pulpitCount * pointsPerPulpit;
        pulpitCount = 0;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
        }
    }

    public void ResetScore()
    {
        currentScore = 0;
        pulpitCount = 0;
        UpdateScoreDisplay();
    }

    public int GetScore()
    {
        return currentScore;
    }
}