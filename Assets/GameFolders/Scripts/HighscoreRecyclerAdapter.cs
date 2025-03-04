using PolyAndCode.UI;
using TMPro;
using UnityEngine;

public class HighscoreRecyclerAdapter : MonoBehaviour , ICell
{
    public TMP_Text usernameText;
    public TMP_Text scoreText;

    private HighscoreModel _highscoreModel;
    private int _cellIndex;

    public void ConfigureCell(HighscoreModel highscore, int cellIndex)
    {
        _cellIndex = cellIndex;
        _highscoreModel = highscore;

        usernameText.text = highscore.Username;
        scoreText.text = highscore.Score.ToString();
    }
}
