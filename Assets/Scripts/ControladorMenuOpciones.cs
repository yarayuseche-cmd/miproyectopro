using UnityEngine;

public class ControladorMenuOpciones : MonoBehaviour
{
    [Header("Panel de Configuración")]
    [SerializeField] private GameObject panelOpciones; // Arrastra aquí el panel oscuro de configuración

    void Start()
    {
        // Asegurarnos de que el panel comience desactivado al iniciar la escena
        if (panelOpciones != null)
        {
            panelOpciones.SetActive(false);
        }
    }

    // Método para abrir el panel (Asignar al botón "Opciones" del menú principal)
    public void AbrirOpciones()
    {
        if (panelOpciones != null)
        {
            panelOpciones.SetActive(true);
        }
    }

    // Método para cerrar el panel (Asignar a un botón de "Cerrar" o "Volver" dentro del panel)
    public void CerrarOpciones()
    {
        if (panelOpciones != null)
        {
            panelOpciones.SetActive(false);
        }
    }
}
