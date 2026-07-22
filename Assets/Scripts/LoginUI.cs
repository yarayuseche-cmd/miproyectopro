using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginUI : MonoBehaviour
{
    [Header("Referencias de UI")]
    public TMP_InputField inputUser;
    public TMP_InputField inputPass;

    [Header("Referencias de Sistema")]
    public DatabaseManager dbManager;

    public void OnClickIniciarSesion()
    {
        // 1. Verificación de seguridad: Comprobamos si las referencias están conectadas
        if (dbManager == null)
        {
            Debug.LogError("Error: El componente 'DatabaseManager' no está asignado en el Inspector.");
            return;
        }

        if (inputUser == null || inputPass == null)
        {
            Debug.LogError("Error: Los InputFields no están asignados en el Inspector.");
            return;
        }

        // 2. Validación en la base de datos
        UserData usuario = dbManager.ValidarLogin(inputUser.text, inputPass.text);

        if (usuario != null)
        {
            Debug.Log("Login exitoso: " + usuario.Username);

            // 3. Persistencia de sesión
            if (SessionManager.Instance != null)
            {
                SessionManager.Instance.usuarioLogueado = usuario.Username;
            }
            else
            {
                Debug.LogWarning("Advertencia: No se encontró el objeto SessionManager en la escena.");
            }

            // 4. Cambio de escena
            SceneManager.LoadScene("MenuPrincipal");
        }
        else
        {
            Debug.LogWarning("Usuario o contraseña incorrectos.");
        }
    }
}