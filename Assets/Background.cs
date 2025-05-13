using UnityEngine;

public class Background : MonoBehaviour
{
    public float scrollSpeed = 0.1f;
    private Mesh mesh;

    public void Start()
    {
        mesh = this.GetComponent<MeshFilter>().mesh;
    }

    public void Update()
    {
        SwapUVs();
    }

    public void SwapUVs()
    {
        Vector2[] uvSwap = mesh.uv;

        for (int b = 0; b < uvSwap.Length; b++)
        {
            uvSwap[b] += new Vector2(0, scrollSpeed * Time.deltaTime);
        }

        mesh.uv = uvSwap;
    }
}
