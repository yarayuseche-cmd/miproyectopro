using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class GoToRegister : MonoBehaviour
{
    // Nombre exacto de tu escena de registro en el Build Settings
    public string escenaRegistro = "EscenaRegistro";

    public void IrARegistro()
    {
        SceneManager.LoadScene(escenaRegistro);
    }
}