using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float bossHp;
    //public float bossClawHpR;
    //public float bossClawHpL;
    public float fFlashTime;

    public bool bossAlive;
    //public Transform FiringPoint;
    //public Transform clawFiringPointLeft;
    //public Transform clawFiringPointRight;
    public BossBulletSpawner _mainCannon;
    public BossBulletSpawner _ClawCannonR;
    public BossBulletSpawner _ClawCannonL;

    public GameObject _target;
    public SpriteRenderer _renderer;
    public AudioSource HitSound;
    public ScoreTrack Score;
    Color origColor;

    public float maxAtkTimer = 1.5f;
    public float atkTimer;

    public float maxSpinAtkTimer = 4.0f;
    public float spinAtkTimer;

    private Coroutine _damageFlashCorotine;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bossAlive = true;
        bossHp = 30;
        fFlashTime = 0.25f;
        origColor = _renderer.color;
        atkTimer = 2f;
        spinAtkTimer = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(atkTimer <= 0)
        {
            //do atk
            Shoot1();
            atkTimer = maxAtkTimer;
        }
        else
        {
            atkTimer -= Time.deltaTime;
        }

        if (spinAtkTimer <= 0)
        {
            //do atk
            Shoot2();
            spinAtkTimer = maxSpinAtkTimer;
        }
        else
        {
            spinAtkTimer -= Time.deltaTime;
        }
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

    private void Shoot1()
    {
       _mainCannon._pool.Get();
       _mainCannon._pool.Get();
       _mainCannon._pool.Get();
       _mainCannon._pool.Get();
       _mainCannon._pool.Get();
    }

    private void Shoot2()
    {
        _ClawCannonR._pool.Get();
        _ClawCannonL._pool.Get();
        
        _ClawCannonR._pool.Get();
        _ClawCannonL._pool.Get();
        
        _ClawCannonR._pool.Get();
        _ClawCannonL._pool.Get();
        
        _ClawCannonR._pool.Get();
        _ClawCannonL._pool.Get();
        
        _ClawCannonR._pool.Get();
        _ClawCannonL._pool.Get();
        
        _ClawCannonR._pool.Get();
        _ClawCannonL._pool.Get();
        
        _ClawCannonR._pool.Get();
        _ClawCannonL._pool.Get();
        
        _ClawCannonR._pool.Get();
        _ClawCannonL._pool.Get();
        
        _ClawCannonR._pool.Get();
        _ClawCannonL._pool.Get();
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
            _renderer.color = lerpedColor;
            yield return null;
        }
        //new Color(255, 165, 0)
    }
}
