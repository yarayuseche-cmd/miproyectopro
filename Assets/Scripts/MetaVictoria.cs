using UnityEngine;
using UnityEngine.SceneManagement;

public class MetaVictoria : MonoBehaviour
{
    [Header("Nombre de la escena de victoria")]
    [SerializeField] private string nombreEscenaVictoria = "Victoria";

    private bool victoriaAlcanzada = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Imprimimos qué objeto tocó la meta para verificar si es el Player
        Debug.Log("Algo tocó la meta: " + other.gameObject.name + " con Tag: " + other.tag);

        if (other.CompareTag("Player") && !victoriaAlcanzada)
        {
            victoriaAlcanzada = true;
            Debug.Log("¡Victoria detectada! Guardando NivelDesbloqueado = 2 en PlayerPrefs.");

            PlayerPrefs.SetInt("NivelDesbloqueado", 2);
            PlayerPrefs.Save();

            // Verificamos de inmediato si se guardó bien
            int comprobacion = PlayerPrefs.GetInt("NivelDesbloqueado", 1);
            Debug.Log("Comprobación PlayerPrefs actual: " + comprobacion);

            Time.timeScale = 1f;
            SceneManager.LoadScene(nombreEscenaVictoria);
        }
    }
}