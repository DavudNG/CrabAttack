using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.ProBuilder.MeshOperations;

public class BossBulletSpawner : MonoBehaviour
{
    public ObjectPool<BossBullet> _pool;
    public BossBullet _bullet;
    public Transform _firepoint;

    public bool _spin = false;
    public int _count = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(_spin)
        {
            _pool = new ObjectPool<BossBullet>(CreateBullet, OnTakeBulletFromPoolType2, OnReturnBulletToPool, OnDestroyBullet, true, 1000, 1500);
        }
        else
        {
            _pool = new ObjectPool<BossBullet>(CreateBullet, OnTakeBulletFromPool, OnReturnBulletToPool, OnDestroyBullet, true, 1000, 1500);
        }
        // on create, take, return, destroy
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void incrementCount()
    {
        _count++;
        if (_count > 4)
        {
            _count = 0;
        }
    }

    private void incrementSpinCount()
    {
        _count++;
        if (_count > 7)
        {
            _count = 0;
        }
    }

    private BossBullet CreateBullet()
    {
        BossBullet bullet = Instantiate(_bullet, _firepoint.position, _firepoint.rotation);

        bullet.SetPool(_pool);

        return bullet;
    }

    public void OnTakeBulletFromPool(BossBullet bullet)
    {
        bullet.transform.position = _firepoint.position;
        switch (_count)
        {
            case 0:
                bullet.transform.forward = _firepoint.forward;
                break;
            case 1:
                bullet.transform.forward =  new Vector3(_firepoint.forward.x + 1.5f, _firepoint.forward.y, _firepoint.forward.z);
                break;
            case 2:
                bullet.transform.forward = new Vector3(_firepoint.forward.x + 0.5f, _firepoint.forward.y, _firepoint.forward.z);
                break;
            case 3:
                bullet.transform.forward = new Vector3(_firepoint.forward.x - 0.5f, _firepoint.forward.y, _firepoint.forward.z);
                break;
            case 4:
                bullet.transform.forward = new Vector3(_firepoint.forward.x - 1.5f, _firepoint.forward.y, _firepoint.forward.z );
                break;
            default:
                break;
        }
        incrementCount();
        bullet.gameObject.SetActive(true);

    }

    public void OnTakeBulletFromPoolType2(BossBullet bullet)
    {
        bullet.transform.position = _firepoint.position;

        switch (_count)
        {
            case 0:
                bullet.transform.forward = _firepoint.forward;
                break;
            case 1:
                bullet.transform.forward = new Vector3(_firepoint.forward.x + 1.5f, _firepoint.forward.y, _firepoint.forward.z);
                break;
            case 2:
                bullet.transform.forward = new Vector3(_firepoint.forward.x + 0.5f, _firepoint.forward.y, _firepoint.forward.z);
                break;
            case 3:
                bullet.transform.forward = new Vector3(_firepoint.forward.x - 0.5f, _firepoint.forward.y, _firepoint.forward.z);
                break;
            case 4:
                bullet.transform.forward = new Vector3(_firepoint.forward.x - 3f, _firepoint.forward.y, _firepoint.forward.z);
                break;
            case 5:
                bullet.transform.forward = new Vector3(_firepoint.forward.x + 3f, _firepoint.forward.y, _firepoint.forward.z);
                break;
            case 6:
                bullet.transform.forward = new Vector3(_firepoint.forward.x + 4.5f, _firepoint.forward.y, _firepoint.forward.z);
                break;
            case 7:
                bullet.transform.forward = new Vector3(_firepoint.forward.x - 4.5f, _firepoint.forward.y, _firepoint.forward.z);
                break;
            default:
                break;
        }
        bullet.gameObject.SetActive(true);
        incrementSpinCount();
    }


    private void OnReturnBulletToPool(BossBullet bullet)
    {
        if (bullet != null && bullet.gameObject.activeSelf)
            bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(BossBullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    // Unity new nuilt in method for object pooling
}
