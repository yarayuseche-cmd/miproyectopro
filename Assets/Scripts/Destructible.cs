using UnityEngine;

public class Destructible : MonoBehaviour
{
    [Header("Configuración de Destrucción")]
    public float tiempoDestruccion = 1f;

    [Header("Configuración de Objetos (Items)")]
    [Range(0f, 1f)]
    public float probabilidadAparicionItem = 0.2f;
    public GameObject[] objetosParaAparecer;

    private void Start()
    {
        // Destruye el objeto después de que pase el tiempo configurado
        Destroy(gameObject, tiempoDestruccion);
    }

    private void OnDestroy()
    {
        // Verifica si hay objetos en la lista y si la suerte está de nuestro lado
        if (objetosParaAparecer.Length > 0 && Random.value < probabilidadAparicionItem)
        {
            // Selecciona un índice aleatorio de la lista de objetos
            int indiceAleatorio = Random.Range(0, objetosParaAparecer.Length);

            // Crea el objeto en la posición actual
            Instantiate(objetosParaAparecer[indiceAleatorio], transform.position, Quaternion.identity);
        }
    }
}