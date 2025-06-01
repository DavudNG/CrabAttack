using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour
{
    public Boss _boss;
    public Ship _player;
    public ScoreTrack _scoreTrack;
    public float _OriginalTimer;
    public float _CountUp;
    private bool EndGame = false;
    public TextMeshProUGUI Timer;
    public TextMeshProUGUI Results;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _OriginalTimer = 99;
        _CountUp = 0; 
    }

    void Reset()
    {
        _CountUp = 0;
        EndGame = false; 
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        if (!EndGame)
        {
            _CountUp += Time.deltaTime;
            Timer.text = "Timer: " + Mathf.Floor(_CountUp);
        }

        if (_CountUp >= _OriginalTimer)
            { 
                EndGame = true;
            }

        if (!_boss.bossAlive)
            EndGame = true;

        if (!_player.bAlive)
            EndGame = true;

        if (EndGame)
        {
            DisplayResults();
            Time.timeScale = 0;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            Restart();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void DisplayResults()
    {
        if (_player.GetStatus() == false)
        {
            Results.text = "GAME OVER: Defeated by GIANT ENEMY CRAB \n Score: " + _scoreTrack.GetScore() + "\n press R to restart or ESC to exit";
        }
        else if (_boss.GetStatus() == false)
        {
            Results.text = "You Win!: Defeated GIANT ENEMY CRAB \n Score: " + _scoreTrack.GetScore() + "\n press R to restart or ESC to exit";
        }
        else
        {
            Results.text = "GAME OVER: Ran out of time \n Score: " + _scoreTrack.GetScore() + "\n press R to restart or ESC to exit";
        }
    }
}
