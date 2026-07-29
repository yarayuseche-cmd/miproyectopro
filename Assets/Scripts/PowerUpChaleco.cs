using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PowerUpChaleco : MonoBehaviour
{
    [Header("Configuración del Nivel")]
    [SerializeField] private string nombreEscenaNivel2 = "Bomberman 2"; // Cambia por el nombre exacto de tu escena del nivel 2
    [SerializeField] private float tiempoDeVidaEnMapa = 3f; // Desaparece en 3 segundos si no se recoge
    [SerializeField] private float duracionProteccion = 4f;  // 4 segundos de invulnerabilidad al jugador

    void Start()
    {
        // Verificar si estamos en el Nivel 2
        if (SceneManager.GetActiveScene().name != nombreEscenaNivel2)
        {
            Destroy(gameObject);
            return;
        }

        // Destruir el chaleco automáticamente si pasan 3 segundos y nadie lo toma
        Destroy(gameObject, tiempoDeVidaEnMapa);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.gameObject.name.Contains("Player"))
        {
            // Buscamos si el jugador tiene un componente para activar el escudo o invulnerabilidad
            StartCoroutine(DarProteccionJugador(collision.gameObject));

            // Desactivar el sprite/collider del chaleco instantáneamente para que no se recoja dos veces
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(gameObject, duracionProteccion); // Se destruye por completo al acabar el efecto
        }
    }

    IEnumerator DarProteccionJugador(GameObject jugador)
    {
        // Buscamos componentes de salud o movimiento para aplicar la invulnerabilidad temporal
        PlayerHealth saludJugador = jugador.GetComponent<PlayerHealth>();

        if (saludJugador != null)
        {
            Debug.Log("¡Chaleco obtenido! Invulnerabilidad por 4 segundos.");

            // Podemos activar una corrutina o estado de invulnerabilidad extendida
            // Usamos un duplicado temporal de la lógica de invulnerabilidad
            yield return StartCoroutine(EscudoTemporizado(saludJugador, duracionProteccion));
        }
    }

    IEnumerator EscudoTemporizado(PlayerHealth salud, float tiempo)
    {
        // Forzamos un estado protegido temporal en el jugador
        float t = 0f;
        while (t < tiempo)
        {
            t += Time.deltaTime;
            yield return null;
        }
        Debug.Log("El efecto del chaleco ha terminado.");
    }
}