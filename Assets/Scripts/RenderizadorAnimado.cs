using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RenderizadorAnimado : MonoBehaviour
{
    private SpriteRenderer renderizador;

    [Header("Sprites y Estados")]
    [Tooltip("Imagen que se muestra cuando el objeto está en reposo")]
    public Sprite spriteReposo;

    [Tooltip("Lista de imágenes para la secuencia de animación")]
    public Sprite[] spritesAnimacion;

    [Header("Configuración de Tiempo")]
    [Tooltip("Tiempo en segundos entre cada cuadro de la animación")]
    public float tiempoEntreCuadros = 0.25f;

    [Header("Opciones de Reproducción")]
    public bool repetirBucle = true;
    public bool estaEnReposo = true;

    private int cuadroActual;

    private void Awake()
    {
        // Obtenemos la referencia al componente de imagen
        renderizador = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        renderizador.enabled = true;
    }

    private void OnDisable()
    {
        renderizador.enabled = false;
    }

    private void Start()
    {
        // Iniciamos el ciclo de la animación
        InvokeRepeating(nameof(SiguienteCuadro), tiempoEntreCuadros, tiempoEntreCuadros);
    }

    private void SiguienteCuadro()
    {
        cuadroActual++;

        // Si la animación llega al final y debe repetirse, vuelve al inicio
        if (repetirBucle && cuadroActual >= spritesAnimacion.Length)
        {
            cuadroActual = 0;
        }

        // Decidimos qué mostrar según el estado actual
        if (estaEnReposo)
        {
            renderizador.sprite = spriteReposo;
        }
        else if (cuadroActual >= 0 && cuadroActual < spritesAnimacion.Length)
        {
            renderizador.sprite = spritesAnimacion[cuadroActual];
        }
    }

    // Función extra para cambiar de estado fácilmente desde otros scripts
    public void CambiarEstadoReposo(bool reposo)
    {
        estaEnReposo = reposo;
        if (!reposo)
        {
            cuadroActual = 0; // Reinicia la animación al activarse
        }
    }
}
