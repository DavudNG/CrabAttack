using UnityEngine;
using TMPro;

public class ScoreTrack : MonoBehaviour
{
    public int score = 0;

    public int ScoreIncrement = 20;

    public TextMeshProUGUI ScoreCount;

    public void Start()
    {
        ScoreCount.text = "Score: " + score;
    }

    public void IncreaseScore()
    {
        score += ScoreIncrement;
        ScoreCount.text = "Score: " + score;
    }
}
