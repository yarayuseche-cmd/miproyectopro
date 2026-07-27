using UnityEngine;
using TMPro; // Necesario para TextMeshPro

public class TemporizadorNivel : MonoBehaviour
{
    [Header("Configuración del Tiempo")]
    [SerializeField] private float tiempoRestante = 120f; // 2:00 minutos iniciales

    [Header("Referencia al Texto")]
    [SerializeField] private TextMeshProUGUI textoTiempo; // Arrastra aquí tu objeto "Texto_Tiempo"

    private bool cronometroActivo = true;

    void Start()
    {
        // Si no lo arrastraste manualmente en el inspector, lo busca automáticamente en los hijos
        if (textoTiempo == null)
        {
            textoTiempo = GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    void Update()
    {
        if (!cronometroActivo) return;

        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            ActualizarReloj(tiempoRestante);
        }
        else
        {
            tiempoRestante = 0;
            cronometroActivo = false;
            ActualizarReloj(0);
            TiempoAgotado();
        }
    }

    private void ActualizarReloj(float segundosTotales)
    {
        if (textoTiempo == null) return;

        int minutos = Mathf.FloorToInt(segundosTotales / 60);
        int segundos = Mathf.FloorToInt(segundosTotales % 60);

        // Muestra el formato exacto M:SS (ejemplo: 2:00)
        textoTiempo.text = string.Format("{0}:{1:00}", minutos, segundos);
    }

    private void TiempoAgotado()
    {
        Debug.Log("¡El tiempo se ha agotado!");

        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");

    }
}
