using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{
    [Header("Nombre de la escena de niveles")]
    [SerializeField] private string nombreEscenaNiveles = "niveles"; // Tal cual como lo tienes en Build Settings

    public void IrANiveles()
    {
        SceneManager.LoadScene(nombreEscenaNiveles);
    }

    public void ReiniciarNivel()
    {
        // Recarga directamente la escena de Bomberman usando su índice (5) o la actual si prefieres
        SceneManager.LoadScene(5); // El índice 5 corresponde a "Scenes/Bomberman" según tu Build Settings
    }
}