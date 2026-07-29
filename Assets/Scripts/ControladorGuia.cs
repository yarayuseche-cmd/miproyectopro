using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorGuia : MonoBehaviour
{
    public void CargarMapaDelJuego()
    {
        SceneManager.LoadScene("Bomberman");
    }
}