using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.ProBuilder.MeshOperations;

public class ShipBulletSpawner : MonoBehaviour
{
    public ObjectPool<Bullet> _pool;
    public Bullet _bullet;
    public Transform _firepoint1;
    public Transform _firepoint2;

    private bool _swap = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // on create, take, return, destroy
        _pool = new ObjectPool<Bullet>(CreateBullet, OnTakeBulletFromPool, OnReturnBulletToPool, OnDestroyBullet, true, 200, 400);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void swapCount()
    {
        _swap = !_swap;
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(_bullet, _firepoint1.position, _firepoint1.rotation);

        bullet.SetPool( _pool );

        return bullet;
    }

    private void OnTakeBulletFromPool(Bullet bullet)
    {
        if(_swap)
        {
            bullet.transform.position = _firepoint1.position;
            bullet.transform.forward = _firepoint1.forward;
            swapCount();
        }

        else
        {
            bullet.transform.position = _firepoint2.position;
            bullet.transform.forward = _firepoint2.forward;
            swapCount();
        }
        bullet.gameObject.SetActive(true);
    }

    private void OnReturnBulletToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    // Unity new nuilt in method for object pooling
}
