using NUnit.Framework.Internal;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static System.Net.WebRequestMethods;


public class Ship : MonoBehaviour
{
    const float MAX_BOUND_X = 9;
    const float MIN_BOUND_X = -9;
    const float MAX_BOUND_Z = 0;
    const float MIN_BOUND_Z = -20;

    public InputSystem_Actions myInputs;
    public Rigidbody rb;
    public ShipBulletSpawner _spawner;
    public GameManager gameManager;

    public Transform myFiringPointA;
    public Transform myFiringPointB;

    public float fSpeed;
    public float fSpeedMod;
    public float fTurnSpeed;
    public float fShotCooldown;
    public float fOriginalShotCooldown;
    float fDamageTime;

    public float fHp;
    public bool  bAlive;
    public bool  bShieldType;
    public bool bCanDamage;

    public AudioSource LaserSound;

    private Vector3 m_EulerAngleVelocity;
    private Coroutine _damageFlashCorotine;

    public SpriteRenderer _renderer;
    public float fFlashTime;
    Color origColor;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        fSpeed = 6.0f;
        fSpeedMod = 1.0f;
        fTurnSpeed = 30.0f;
        fOriginalShotCooldown = 0.05f;
        fHp = 1.0f;
        fFlashTime = 0.5f;
        bAlive = true;
        bCanDamage = true;
        bShieldType = false;
        fDamageTime = fFlashTime;
        fShotCooldown = fOriginalShotCooldown;
        _spawner = this.GetComponent<ShipBulletSpawner>();
        rb = this.GetComponent<Rigidbody>();
        origColor = _renderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x > MAX_BOUND_X || this.gameObject.transform.position.x < MIN_BOUND_X || gameObject.transform.position.z > MAX_BOUND_Z || gameObject.transform.position.z < MIN_BOUND_Z)
        {
            gameObject.transform.position = new Vector3(Mathf.Clamp(this.gameObject.transform.position.x, MIN_BOUND_X, MAX_BOUND_X), gameObject.transform.position.y, Mathf.Clamp(this.gameObject.transform.position.z, MIN_BOUND_Z, MAX_BOUND_Z));
        }

        if (fShotCooldown >= 0)
            fShotCooldown -= Time.deltaTime;

        //if (fDamageTime < fFlashTime)
        //{
        //    bCanDamage = false;
        //    fDamageTime += Time.deltaTime;
        //}
        //else
        //{
        //    bCanDamage = true;
        //}
    }
    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(-m_EulerAngleVelocity * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);

        if (rb.linearVelocity == Vector3.zero)
        {
            m_EulerAngleVelocity = Vector3.zero;
            rb.MoveRotation(Quaternion.identity);
        }
    }

    void OnMove(InputValue inputValue)
    {
        rb.linearVelocity = inputValue.Get<Vector3>() * fSpeed * fSpeedMod;

        //Set the angular velocity of the Rigidbody (rotating around the Y axis, 100 deg/sec)
        m_EulerAngleVelocity = new Vector3(0, 0, inputValue.Get<Vector3>().x) * fTurnSpeed;
    }

    void OnShoot()
    {
        if(fShotCooldown <= 0)
        {
            LaserSound.pitch = Random.Range(0.8f, 1.2f);
            LaserSound.Play();
            // TODO SHOOT
            fShotCooldown = fOriginalShotCooldown;
            _spawner._pool.Get();
        }
    }

    //void onRestart()
    //{
    //    gameManager.Restart();
    //}

    void OnShieldSwap()
    {
        SwapShield();
    }

    void SwapShield()
    {
        bShieldType = !bShieldType;
    }


    public bool GetShieldType() { return bShieldType; }
    public bool GetStatus() { return bAlive; }
    public float GetHp() { return fHp; }

    public void TakeDamage()
    {
        if (bCanDamage)
        {
            //fDamageTime = 0;
            fHp -= 1;
            //CallDamageFlash();
        }
        
        if (fHp <= 0)
            bAlive = false;
    }

    //public void CallDamageFlash()
    //{
    //    _damageFlashCorotine = StartCoroutine(DamageFlasher());
    //}
    //
    //private IEnumerator DamageFlasher()
    //{
    //    float elapsedTime = 0f;
    //
    //    while (elapsedTime < fFlashTime)
    //    {
    //        elapsedTime += Time.deltaTime;
    //
    //        Color lerpedColor = Color.Lerp(Color.white, origColor, elapsedTime / fFlashTime);
    //        _renderer.color = lerpedColor;
    //        yield return null;
    //    }
    //}
}
