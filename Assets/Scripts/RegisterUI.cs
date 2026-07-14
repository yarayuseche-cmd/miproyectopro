using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class RegisterUI : MonoBehaviour
{
    public TMP_InputField inputUsername;
    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;
    public DatabaseManager dbManager;

    // Asegúrate de que este nombre sea EXACTAMENTE igual al de tu Build Settings
    // En tu caso, es "MenuPrincipal" (sin el prefijo "Scenes/" en el nombre de la escena)
    public string nombreEscenaInicio = "MenuPrincipal";

    public void OnClickRegistrar()
    {
        string user = inputUsername.text;
        string email = inputEmail.text;
        string pass = inputPassword.text;

        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
        {
            Debug.LogWarning("Por favor, rellena todos los campos.");
            return;
        }

        // Intentamos registrar
        bool registrado = dbManager.RegistrarUsuario(user, email, pass);

        if (registrado)
        {
            Debug.Log("¡Registro exitoso! Cargando menú...");

            // Esta línea hace el cambio de escena
            SceneManager.LoadScene(nombreEscenaInicio);
        }
        else
        {
            Debug.LogError("El usuario ya existe o hubo un error al guardar.");
        }
    }
}