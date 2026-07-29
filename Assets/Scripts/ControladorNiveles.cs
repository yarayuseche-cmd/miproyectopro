using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorNiveles : MonoBehaviour
{
    [Header("Configuración de UI")]
    [Tooltip("Arrastra los botones de los niveles en orden (Nivel 1, Nivel 2...)")]
    public Button[] botonesNiveles;

    void OnEnable()
    {
        ActualizarBotonesNiveles();
    }

    void Start()
    {
        ActualizarBotonesNiveles();
    }

    public void ActualizarBotonesNiveles()
    {
        // Si nunca se ha guardado nada, por defecto devuelve 1 (solo nivel 1 desbloqueado)
        int nivelDesbloqueado = PlayerPrefs.GetInt("NivelDesbloqueado", 1);

        // Recorremos los botones para activarlos o bloquearlos automáticamente
        for (int i = 0; i < botonesNiveles.Length; i++)
        {
            int numeroDeNivel = i + 1;

            if (numeroDeNivel <= nivelDesbloqueado)
            {
                botonesNiveles[i].interactable = true; // Desbloqueado
            }
            else
            {
                botonesNiveles[i].interactable = false; // Bloqueado
            }
        }
    }

    // Método para cargar la escena
    public void CargarEscenaNivel(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }

    // [ContextMenu] crea un botón directo en los tres puntos (...) del componente en el Inspector
    [ContextMenu("Borrar Progreso (Bloquear Nivel 2)")]
    public void BorrarProgreso()
    {
        PlayerPrefs.DeleteKey("NivelDesbloqueado");
        PlayerPrefs.Save();
        ActualizarBotonesNiveles();
        Debug.Log("¡Progreso borrado! El nivel 2 vuelve a estar bloqueado.");
    }
}