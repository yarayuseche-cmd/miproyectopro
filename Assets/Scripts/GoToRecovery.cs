using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar de escena

public class GoToRecovery : MonoBehaviour
{
    // Nombre exacto de tu escena de recuperación de contraseña
    public string nombreEscenaRecuperar = "Recuperarcontra";

    public void IrARecuperar()
    {
        SceneManager.LoadScene(nombreEscenaRecuperar);
    }
}
