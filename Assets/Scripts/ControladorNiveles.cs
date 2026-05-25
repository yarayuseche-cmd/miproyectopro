using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladoresNiveles : MonoBehaviour
{
    [Header("Configuración de UI")]
    [Tooltip("Arrastra los botones de los niveles en orden (Nivel 1, Nivel 2, Nivel 3...)")]
    public Button[] botonesNiveles;

    void Start()
    {
        // PlayerPrefs guarda el nivel máximo alcanzado por el jugador.
        // Si es la primera vez que juega, por defecto será el nivel 1.
        int nivelDesbloqueado = PlayerPrefs.GetInt("NivelDesbloqueado", 1);

        // Recorremos todos los botones de la lista
        for (int i = 0; i < botonesNiveles.Length; i++)
        {
            // El índice de la lista empieza en 0, así que el Nivel 1 es i + 1.
            int numeroDeNivel = i + 1;

            if (numeroDeNivel <= nivelDesbloqueado)
            {
                // Si el nivel es menor o igual al desbloqueado, el botón se puede presionar
                botonesNiveles[i].interactable = true;
            }
            else
            {
                // Si no, el botón se bloquea automáticamente
                botonesNiveles[i].interactable = false;
            }
        }
    }

    // Esta función la llamará cada botón al hacerle click (se asigna en el OnClick del botón)
    public void SeleccionarNivel(int numeroDeNivel)
    {
        // Guardamos cuál es el nivel que se va a jugar actualmente
        PlayerPrefs.SetInt("NivelActual", numeroDeNivel);

        // CARGA DE ESCENA ACTUALIZADA: 
        // Cambiado a "Bomberman" para que coincida exactamente con tu escena de juego principal.
        SceneManager.LoadScene("Bomberman");
    }

    // Método para vaciar los datos guardados y volver a empezar desde el Nivel 1
    public void BorrarProgreso()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarga el menú actual
    }
}