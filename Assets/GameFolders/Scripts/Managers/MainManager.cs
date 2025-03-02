using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    [SerializeField] private GameObject HighScorePanel;
    [SerializeField] private GameObject MainPanel;

    private void Start()
    {
        MainPanel.SetActive(true);
        HighScorePanel.SetActive(false);
    }
    public void PlayFunc()
    {
        SceneManager.LoadSceneAsync("GameScene");
    }

    public void HighscoresButtonFunc()
    {
        HighScorePanel.SetActive(true);
        MainPanel.SetActive(false);
    }
    public void BackButtonFunc()
    {
        HighScorePanel.SetActive(false);
        MainPanel.SetActive(true);
    }
}
