using UnityEngine;
using UnityEngine.SceneManagement;

public class MetaVictoria : MonoBehaviour
{
    [Header("Nombre de la escena de victoria")]
    [SerializeField] private string nombreEscenaVictoria = "Victoria"; // Escribe aquí el nombre exacto de tu escena de victoria

    private bool victoriaAlcanzada = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !victoriaAlcanzada)
        {
            victoriaAlcanzada = true;
            Debug.Log("¡Victoria alcanzada! Cargando escena de victoria...");

            // Descongelamos el tiempo por si acaso estaba pausado
            Time.timeScale = 1f;

            // Cargamos directamente la escena de victoria
            SceneManager.LoadScene(nombreEscenaVictoria);
        }
    }
}