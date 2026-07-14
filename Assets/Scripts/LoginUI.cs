using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class LoginUI : MonoBehaviour
{
    public TMP_InputField inputUser;
    public TMP_InputField inputPass;
    public DatabaseManager dbManager;

    public void OnClickLogin()
    {
        // Llamamos a la función de validación que definimos en DatabaseManager
        UserData usuario = dbManager.ValidarLogin(inputUser.text, inputPass.text);

        if (usuario != null)
        {
            // Login exitoso
            Debug.Log("¡Bienvenido, " + usuario.Username + "!");

            // Guardamos el usuario en la sesión global
            GameSession.UsuarioActual = usuario;

            // CAMBIO DE ESCENA:
            // Asegúrate de que el nombre sea exactamente "MenuPrincipal"
            SceneManager.LoadScene("MenuPrincipal");
        }
        else
        {
            // Login fallido
            Debug.LogError("Usuario o contraseña incorrectos.");
        }
    }
}