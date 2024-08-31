using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject startScreen;
    public GameObject gameOverScreen;
    public TextMeshProUGUI finalScoreText;
    public Button startButton;
    public Button restartButton;

    public PulpitManager pulpitManager;
    public DoofusController doofusController;
    public ScoreManager scoreManager;

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

    public void Start()
    {
        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame);
        ShowStartScreen();
        
        // Disable start button until data is loaded
        startButton.interactable = false;
        
        StartCoroutine(WaitForDataAndInitialize());

    }

    IEnumerator WaitForDataAndInitialize()
    {
        // Wait for GameDataManager to be initialized
        while (GameDataManager.Instance == null)
        {
            yield return null;
        }

        // Now wait for data to be loaded
        while (!GameDataManager.Instance.isDataLoaded)
        {
            yield return null;
        }
        
        // Data is loaded, enable start button
        startButton.interactable = true;
    }

    public void ShowStartScreen()
    {
        startScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        Time.timeScale = 0; // Pause the game
    }

    public void StartGame()
    {

        if (GameDataManager.Instance == null || !GameDataManager.Instance.isDataLoaded)
        {
            Debug.LogWarning("Attempting to start game before data is loaded.");
            return;
        }
        startScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        Time.timeScale = 1; // Resume the game
        pulpitManager.StartGame();
        doofusController.DoofusStartGame();
        scoreManager.ResetScore();
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        finalScoreText.text = "Final Score: " + scoreManager.GetScore();
        Time.timeScale = 0; // Pause the game
    }

    public void RestartGame()
    {
        StartGame();
    }
}