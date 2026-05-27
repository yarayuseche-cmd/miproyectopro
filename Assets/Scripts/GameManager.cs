using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instancia { get; private set; }

    private GameObject[] jugadores;

    private void Awake()
    {
        if (Instancia != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instancia = this;
        }
    }

    private void OnDestroy()
    {
        if (Instancia == this)
        {
            Instancia = null;
        }
    }

    private void Start()
    {
        // Busca a los jugadores por su etiqueta (Tag). 
        // Asegúrate de que en Unity tus jugadores tengan el tag "Player".
        jugadores = GameObject.FindGameObjectsWithTag("Player");
    }

    public void ComprobarEstadoVictoria()
    {
        int conteoVivos = 0;

        for (int i = 0; i < jugadores.Length; i++)
        {
            if (jugadores[i].activeSelf)
            {
                conteoVivos++;
            }
        }

        if (conteoVivos == 0)
        {
            // El jugador murió, se acabó la partida
            Invoke(nameof(NuevaRonda), 3f);
        }
    }

    private void NuevaRonda()
    {
        // Recarga la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}