using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreUI : MonoBehaviour
{
    [SerializeField]private GameObject rowPrefab;
    [SerializeField]private Transform contentParent;
    [SerializeField] private HighscoreManager hsManager;
    private Queue<GameObject> rowPool = new Queue<GameObject>();

    private void Start()
    {
        GetHighscoreFromManager();
    }

    public async void GetHighscoreFromManager()
    {
        List<(string username, int score)> highScores = await hsManager.LoadHighScores();

        UpdateHighScoreUI(highScores);
    }

    public void UpdateHighScoreUI(List<(string username, int score)> highScores)
    {
        while (rowPool.Count > 0)
        {
            GameObject obj = rowPool.Dequeue();
            obj.SetActive(false);
        }

        foreach (var scoreData in highScores)
        {
            GameObject row;
            if (rowPool.Count > 0)
            {
                row = rowPool.Dequeue();
                row.SetActive(true);
            }
            else
            {
                row = Instantiate(rowPrefab, contentParent);
            }

            TMP_Text[] textComponents = row.GetComponentsInChildren<TMP_Text>();

            foreach (TMP_Text textComponent in textComponents)
            {
                switch (textComponent.gameObject.tag)
                {
                    case "HighscoreItemUsername":
                        textComponent.text = scoreData.username;
                        break;
                    case "HighscoreItemScore":
                        textComponent.text = scoreData.score.ToString();
                        break;
                }
            }

            rowPool.Enqueue(row);
        }
    }
}

