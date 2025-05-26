using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float fSpeed;
    public float fSpeedMod;
    public float fDestroyTime;
    public float fBulletDamage;
    private bool bHasCollided = false;

    private ObjectPool<Bullet> _pool;
    private Bullet _bullet;
    public ShipBulletSpawner _spawner;

    private Coroutine DeactivateBulletAfterTimeCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        fSpeed = 10.0f;
        fSpeedMod = 1;
        fDestroyTime = 5f;
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
        gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.forward * fSpeed * fSpeedMod;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!bHasCollided)
        {
            if (other.tag == "Boss")
            {
                other.GetComponentInParent<Boss>().takeDamage(fBulletDamage);
                // add impact sound here
                bHasCollided = true;
                Remove();
            }
            else if (other.tag == "Killzone")
            {
                //Debug.Log("culled");
                bHasCollided = true;
                Remove();
            }
        }
    }

    public void SetPool(ObjectPool<Bullet> pool)
    {
        _pool = pool;
    }

    private void Remove()
    {
        _pool.Release(this);
    }

    private IEnumerator DeactivateBulletAfterTime()
    {
        float elapsedTime = 0f;
        while(elapsedTime < fDestroyTime)
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
