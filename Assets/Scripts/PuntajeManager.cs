using UnityEngine;
using TMPro; // Necesario para usar texto de TextMeshPro

public class PuntajeManager : MonoBehaviour
{
    // Patrón Singleton para acceder fácilmente desde cualquier script de enemigo o bomba
    public static PuntajeManager Instance;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI textoPuntaje; // Arrastra aquí tu texto del Canvas

    private int puntos = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ActualizarTextoUI();
    }

    public void SumarPuntos(int cantidad)
    {
        puntos += cantidad;
        ActualizarTextoUI();
    }

    private void ActualizarTextoUI()
    {
        if (textoPuntaje != null)
        {
            textoPuntaje.text = "Puntos: " + puntos;
        }
    }
}