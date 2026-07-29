using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDesplegableNiveles : MonoBehaviour
{
    [Header("Referencia al Panel Desplegable")]
    [SerializeField] private GameObject menuFlotante;

    private bool estaAbierto = false;

    // Método para abrir o cerrar el menú al presionar el botón azul
    public void ToggleMenu()
    {
        estaAbierto = !estaAbierto;
        if (menuFlotante != null)
        {
            menuFlotante.SetActive(estaAbierto);
        }
    }

    // Opciones del menú (puedes cambiar los nombres de las escenas según las tuyas)
    public void IrAHistorial(string nombreEscenaHistorial)
    {
        SceneManager.LoadScene(nombreEscenaHistorial);
    }

    public void IrAAjustes(string nombreEscenaAjustes)
    {
        SceneManager.LoadScene(nombreEscenaAjustes);
    }

    public void CerrarSesion(string nombreEscenaLogin)
    {
        SceneManager.LoadScene(nombreEscenaLogin);
    }
}
