using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float fSpeed;
    public float fSpeedMod;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fSpeed = 10.0f;
        fSpeedMod = 1;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = this.gameObject.transform.position += new Vector3(0, 0, fSpeed * fSpeedMod * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision");
        this.gameObject.SetActive(false);
    }
}
