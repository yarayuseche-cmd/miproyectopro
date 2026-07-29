using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaManager : MonoBehaviour
{
    [Header("Panel de Pausa")]
    [SerializeField] private GameObject panelPausa;

    private bool juegoPausado = false;

    void Update()
    {
        // Opcional: También puedes pausar con la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Pausar()
    {
        juegoPausado = true;
        if (panelPausa != null) panelPausa.SetActive(true);
        Time.timeScale = 0f; // Congela el tiempo del juego
    }

    public void Reanudar()
    {
        juegoPausado = false;
        if (panelPausa != null) panelPausa.SetActive(false);
        Time.timeScale = 1f; // Restaura el tiempo normal
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f; // Importante restaurar el tiempo antes de cambiar de escena
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void VolverACasa(string nombreEscenaMenu)
    {
        Time.timeScale = 1f; // Importante restaurar el tiempo
        SceneManager.LoadScene(nombreEscenaMenu);
    }
}