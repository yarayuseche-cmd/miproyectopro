using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class LogoutButton : MonoBehaviour
{
    // Escribe aquí el nombre exacto de tu escena de Inicio de Sesión
    public string nombreEscenaLogin = "EscenaLogin";

    public void SalirAlLogin()
    {
        // Opcional: Limpiar la sesión actual si la estás guardando
        GameSession.UsuarioActual = null;

        // Cargar la escena de login
        SceneManager.LoadScene(nombreEscenaLogin);
    }
}
