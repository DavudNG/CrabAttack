using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class BossBullet : MonoBehaviour
{
    public float fSpeed;
    public float fSpeedMod;
    public float fDestroyTime;
    public float fBulletDamage;
    private bool bHasCollided = false;

    private ObjectPool<BossBullet> _pool;
    private BossBullet _bullet;
    //public ShipBulletSpawner _spawner;

    private Coroutine DeactivateBulletAfterTimeCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        fSpeed = 10.0f;
        fSpeedMod = 1;
        fDestroyTime = 2f;
        fBulletDamage = 1.0f;
        _bullet = this;
        DeactivateBulletAfterTimeCoroutine = StartCoroutine(DeactivateBulletAfterTime());
    }

    // put the stuff here for when it wakes up each time
    private void OnEnable()
    {
        bHasCollided = false;
        _bullet = this;
        SetVelocity();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetVelocity()
    {
        gameObject.GetComponent<Rigidbody>().linearVelocity = this.transform.forward * fSpeed * fSpeedMod;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!bHasCollided)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<Ship>().TakeDamage();
            }

            else if (other.tag == "Killzone")
            {
                //Debug.Log("culled");
                bHasCollided = true;
                Remove();
            }
        }
    }

    public void SetPool(ObjectPool<BossBullet> pool)
    {
        _pool = pool;
    }

    public void SetFiringpoint()
    {
        this.transform.position = Vector3.zero;
    }

    public void SetForward(Vector3 forward)
    {
        this.transform.forward = forward;
    }

    private void Remove()
    {
        _pool.Release(this);
    }

    private IEnumerator DeactivateBulletAfterTime()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fDestroyTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Remove();
    }

    void PlaySound()
    {
        // INSERT PLAY SOUND FUNCTION HERE
    }
}
