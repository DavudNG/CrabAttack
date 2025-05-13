using UnityEngine;
using UnityEngine.InputSystem;

public class Ship : MonoBehaviour
{
    public float fSpeed;
    public float fSpeedMod;
    public InputActionAsset myInputs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fSpeed = 3.0f;
        fSpeedMod = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnLeft()
    {
        this.transform.position += Vector3.left * fSpeed * Time.deltaTime;
    }

    void OnRight()
    {
        this.transform.position += Vector3.right * fSpeed * Time.deltaTime;
    }

    void OnUp()
    {
        this.transform.position += Vector3.forward * fSpeed * Time.deltaTime;
    }

    void OnDown()
    {
        this.transform.position += Vector3.back * fSpeed * Time.deltaTime;
    }

    void OnShoot()
    {

    }

    void OnShieldSwap()
    {

    }
}
