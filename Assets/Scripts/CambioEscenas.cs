using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscenas : MonoBehaviour
{
    void Start()
    {
        // Al arrancar el juego, comprueba si el usuario ya guardó sesión
        if (PlayerPrefs.HasKey("UsuarioLogueado"))
        {
            // Va directo al juego
            SceneManager.LoadScene("MenuPrincipal");
        }
    }

    // Funciones para los botones de la pantalla principal
    public void IrALogin()
    {
        SceneManager.LoadScene("EscenaLogin");
    }

    public void IrARegistro()
    {
        SceneManager.LoadScene("EscenaRegistro");
    }
}
