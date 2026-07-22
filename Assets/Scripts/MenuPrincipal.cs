using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    [Header("Configuración de Escena")]
    [Tooltip("Escribe aquí el nombre exacto de tu escena de niveles")]
    public string escenaNiveles = "Niveles";

    public void OnClickJugar()
    {
        // Red de seguridad: verificamos que no esté vacío el campo
        if (string.IsNullOrEmpty(escenaNiveles))
        {
            Debug.LogError("¡Error! El nombre de la escena de niveles está vacío en el Inspector de MenuPrincipal.");
            return;
        }

        Debug.Log("Cargando escena: " + escenaNiveles);

        // Cargamos la escena de niveles
        SceneManager.LoadScene(escenaNiveles);
    }
}
