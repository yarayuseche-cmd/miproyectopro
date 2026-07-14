using UnityEngine;
using UnityEngine.SceneManagement; // Importante para cambiar de escena

public class BackButton : MonoBehaviour
{
    // Nombre de la escena a la que quieres volver
    public string nombreEscenaDestino = "EscenaConexion";

    public void VolverAtras()
    {
        SceneManager.LoadScene(nombreEscenaDestino);
    }
}
