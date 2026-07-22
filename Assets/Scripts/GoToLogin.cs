using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para gestionar escenas

public class GoToLogin : MonoBehaviour
{
    // Escribe el nombre exacto de tu escena de Login según el Build Settings
    public string nombreEscenaLogin = "EscenaLogin";

    public void IrALogin()
    {
        SceneManager.LoadScene(nombreEscenaLogin);
    }
}
