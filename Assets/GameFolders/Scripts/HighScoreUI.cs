using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HighScoreUI : MonoBehaviour
{
    [SerializeField] private GameObject rowPrefab;
    [SerializeField] private Transform contentParent;
    [SerializeField] HighscoreManager hsManager;
    private Queue<GameObject> rowPool = new Queue<GameObject>();
    private List<HighscoreModel> _highScores = new List<HighscoreModel>();

    private void Start()
    {
        GetHighscoreFromManager();
    }
    public async void GetHighscoreFromManager()
    {
        _highScores = await hsManager.LoadHighScores();
        UpdateHighScoreUI(_highScores);
    }

    public void UpdateHighScoreUI(List<HighscoreModel> highScores)
    {
        int index = 0;

        foreach (var scoreData in highScores)
        {
            GameObject row;

            if (index < rowPool.Count)
            {
                row = rowPool.Dequeue();
                row.SetActive(true);
            }
            else
            {
                row = Instantiate(rowPrefab, contentParent);
            }

            Outline[] outlines = row.GetComponentsInChildren<Outline>();
            TMP_Text[] textComponents = row.GetComponentsInChildren<TMP_Text>();

            
            Color outlineColor;
            Color textColor;

            switch (index)
            {
                case 0:
                    outlineColor = new Color32(255, 215, 0, 255);
                    textColor = new Color32(255, 215, 0, 255);
                    break;
                case 1:
                    outlineColor = new Color32(192, 192, 192, 255);
                    textColor = new Color32(192, 192, 192, 255);
                    break;
                case 2:
                    outlineColor = new Color32(205, 127, 50, 255);
                    textColor = new Color32(205, 127, 50, 255);
                    break;
                default:
                    outlineColor = new Color32(255, 255, 255, 255);
                    textColor = new Color32(255, 255, 255, 255);
                    break;
            }

            foreach (Outline outline in outlines)
            {
                outline.effectColor = outlineColor;
            }

            foreach (TMP_Text textComponent in textComponents)
            {
                textComponent.color = textColor;

                switch (textComponent.gameObject.tag)
                {
                    case "HighscoreItemUsername":
                        textComponent.text = scoreData.Username;
                        break;
                    case "HighscoreItemScore":
                        textComponent.text = scoreData.Score.ToString();
                        break;
                    case "HighscoreItemIndex":
                        textComponent.text = (index + 1).ToString();
                        break;
                }
            }

            rowPool.Enqueue(row);
            index++;
        }

        while (index < rowPool.Count)
        {
            GameObject obj = rowPool.Dequeue();
            obj.SetActive(false);
        }
    }


}

