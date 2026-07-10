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
        // Solo avanzamos si no estamos en reposo
        if (!estaEnReposo)
        {
            cuadroActual++;

            // Lógica de bucle
            if (repetirBucle && cuadroActual >= spritesAnimacion.Length)
            {
                cuadroActual = 0;
            }

            // Asignación de sprite si estamos dentro de los límites
            if (cuadroActual >= 0 && cuadroActual < spritesAnimacion.Length)
            {
                renderizador.sprite = spritesAnimacion[cuadroActual];
            }
        }
        else
        {
            // Si estamos en reposo, mostramos el sprite de reposo
            renderizador.sprite = spriteReposo;
        }
    }

    public void CambiarEstadoReposo(bool reposo)
    {
        estaEnReposo = reposo;
        if (!reposo)
        {
            cuadroActual = 0; // Reinicia al salir de reposo
        }
    }

    public void ReproducirAnimacionMuerte()
    {
        estaEnReposo = false;   // Forzamos salida de reposo
        repetirBucle = false;    // Desactivamos el bucle para que se vea solo una vez
        cuadroActual = 0;        // Reiniciamos al primer frame

        // Forzamos visualización inmediata del primer frame de muerte
        if (spritesAnimacion != null && spritesAnimacion.Length > 0)
        {
            renderizador.sprite = spritesAnimacion[0];
        }
    }

    public float ObtenerTiempoAnimacionMuerte()
    {
        return spritesAnimacion.Length * tiempoEntreCuadros;
    }
}
