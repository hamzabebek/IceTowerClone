using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private UnityEngine.Transform target;
    [SerializeField] private float smoothSpeed = 0.2f;
    private float cameraSpeed = 1f;
    private float triggerHeight = 12f;
    private float highestY;
    private float scoreY;



    void Start()
    {
        highestY = target.position.y + 10f;
    }

    void Update()
    {
        Scoring();
        if (triggerHeight < target.position.y)
        {
            if (target.position.y > highestY)
            {
                scoreY = highestY;
                highestY = target.position.y;
                camPos();
            }
            else
            {
                highestY += cameraSpeed * Time.deltaTime;
                camPos();
            }
        }
    }
    private void Scoring()
    {
        if (target.position.y > scoreY)
        {
            scoreY = target.position.y;
            FindObjectOfType<GameManager>().UpdateScoreText((int)scoreY / 3);
        }
        if ((int)scoreY/3 > PlayerPrefs.GetInt("highScore"))
        {
            PlayerPrefs.SetInt("highScore", (int)scoreY/3);
            FindObjectOfType<HighscoreManager>().SaveHighScoreFunc((int)scoreY / 3);
        }
    }
    private void camPos()
    {
        Vector3 newPosCam = new Vector3(transform.position.x, highestY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosCam, smoothSpeed);
    }
}
