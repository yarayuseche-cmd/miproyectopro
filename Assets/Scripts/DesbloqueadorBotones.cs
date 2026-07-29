using UnityEngine;
using UnityEngine.UI;

public class DesbloqueadorBotones : MonoBehaviour
{
    public Button botonNivel2; // Arrastra aquí el botón del Nivel 2 desde el Inspector

    void Start()
    {
        // Revisamos si el nivel 2 fue desbloqueado previamente (por defecto vale 0 si nunca se ha guardado)
        int nivel2Desbloqueado = PlayerPrefs.GetInt("Nivel2Desbloqueado", 0);

        if (botonNivel2 != null)
        {
            if (nivel2Desbloqueado == 1)
            {
                // Si ya ganó el nivel 1, habilitamos el botón
                botonNivel2.interactable = true;
            }
            else
            {
                // Si no lo ha ganado, el botón se mantiene bloqueado
                botonNivel2.interactable = false;
            }
        }
    }
}
