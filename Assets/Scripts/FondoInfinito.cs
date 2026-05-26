using UnityEngine;

public class FondoInfinito : MonoBehaviour
{
    public float velocidadX = 0.1f;
    public float velocidadY = 0f;
    private Material miMaterial;

    void Start()
    {
        miMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // Desplaza el offset del material con el tiempo
        Vector2 offset = miMaterial.mainTextureOffset;
        offset.x += velocidadX * Time.deltaTime;
        offset.y += velocidadY * Time.deltaTime;
        miMaterial.mainTextureOffset = offset;
    }
}
