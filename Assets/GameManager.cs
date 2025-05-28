using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public GameObject _boss;
    public GameObject _player;
    public float _OriginalTimer;
    public float _CountUp;
    private bool EndGame = false;
    public TextMeshProUGUI Timer;

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
           

    }
}
