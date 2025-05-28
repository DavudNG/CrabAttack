using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float bossHp;
    //public float bossClawHpR;
    //public float bossClawHpL;
    public float fFlashTime;

    public bool bossAlive;
    public Transform FiringPoint;
    public Transform clawFiringPointLeft;
    public Transform clawFiringPointRight;
    public GameObject _target;
    public MeshRenderer _renderer;
    public AudioSource HitSound;
    public ScoreTrack Score;
    Color origColor;

    private Coroutine _damageFlashCorotine;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bossAlive = true;
        bossHp = 500;
        fFlashTime = 0.25f;
        origColor = _renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void updateTargetVector()
    {
        // lock on to player
    }

    public void takeDamage(float damage)
    {
        HitSound.pitch = Random.Range(0.8f, 1.2f);
        HitSound.Play();
        bossHp -= damage;
        CallDamageFlash();
        Score.IncreaseScore();

        if (bossHp <= 0)
        {
            bossAlive = false;
        }
    }

    public bool GetStatus() { return bossAlive; }

    public void CallDamageFlash()
    {
        _damageFlashCorotine = StartCoroutine(DamageFlasher());
    }

    private IEnumerator DamageFlasher()
    {
        //float currAmount = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < fFlashTime)
        {
            elapsedTime += Time.deltaTime;
            //currAmount = Mathf.Lerp(1f, 0.5f, elapsedTime/ fFlashTime);
            //_renderer.material.SetFloat("_Metallic", currAmount);
            
            Color lerpedColor = Color.Lerp(Color.red, origColor, elapsedTime / fFlashTime);
            _renderer.material.color = lerpedColor;
            yield return null;
        }
        //new Color(255, 165, 0)
    }
}
