using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour

{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }
    private void Start()
    {
        scoreText.text = "0";
        highScoreText.text = "High Score : " + PlayerPrefs.GetInt("highScore");
    }
    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
        highScoreText.text = "High Score : " + PlayerPrefs.GetInt("highScore");
    }
    public void RestartGame()
    {
        SceneManager.LoadSceneAsync("GameScene");
    }
    public void LoadMainMenuScene()
    {
        SceneManager.LoadSceneAsync("MainMenuScene");
    }
}
