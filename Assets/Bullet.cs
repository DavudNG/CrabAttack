using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
public class Bullet : MonoBehaviour
{
    public float fSpeed;
    public float fSpeedMod;
    public float destroyTime;

    private ObjectPool<Bullet> _pool;
    private Bullet _bullet;
    public ShipBulletSpawner _spawner;

    private Coroutine DeactivateBulletAfterTimeCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        fSpeed = 10.0f;
        fSpeedMod = 1;
        destroyTime = 5f;
        _bullet = this;
        DeactivateBulletAfterTimeCoroutine = StartCoroutine(DeactivateBulletAfterTime());
    }

    // put the stuff here for when it wakes up each time
    private void OnEnable()
    {
        SetVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        //this.gameObject.transform.position = this.gameObject.transform.position += new Vector3(0, 0, fSpeed * fSpeedMod * Time.deltaTime);
    }

    private void SetVelocity()
    {
        gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.forward * fSpeed * fSpeedMod;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Killzone" || other.tag == "Boss")
        {
            Debug.Log("collision");
            Remove();
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
        while(elapsedTime < destroyTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Remove();
    }
}
