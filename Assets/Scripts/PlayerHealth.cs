using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Configuración del Jugador")]
    [Range(1, 4)][SerializeField] private int numeroJugador = 1;
    [SerializeField] private int vidasActuales = 3;

    [Header("Posición de Reaparición")]
    [SerializeField] private Transform puntoRespawn;

    [Header("Ajustes de Daño")]
    [SerializeField] private float tiempoInvulnerabilidad = 1.5f;

    [Header("Referencias Visuales (Sprites Hijos)")]
    [SerializeField] private GameObject spriteDeath;        // Arrastra aquí el objeto "Death"
    [SerializeField] private GameObject[] spritesNormales;  // Arrastra Up, Down, Left, Right

    private BarraVidasUI barraVidasUI;
    private bool esInvulnerable = false;
    private Collider2D miCollider;

    void Start()
    {
        vidasActuales = 3;
        miCollider = GetComponent<Collider2D>();

        // Buscar la barra de vida en la escena
        barraVidasUI = FindObjectOfType<BarraVidasUI>();

        if (barraVidasUI != null)
        {
            barraVidasUI.ActualizarBarra(vidasActuales);
        }

        // Estado inicial seguro
        ActivarSpritesNormales(true);
        if (spriteDeath != null) spriteDeath.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Enemigo") || collision.gameObject.CompareTag("Enemigo"))
        {
            RecibirDaño();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Enemigo") || collision.CompareTag("Enemigo") || collision.CompareTag("Explosion"))
        {
            RecibirDaño();
        }
    }

    public void RecibirDaño()
    {
        if (esInvulnerable) return;

        // Activamos la invulnerabilidad y desactivamos el collider al instante para evitar doble toque
        esInvulnerable = true;
        if (miCollider != null) miCollider.enabled = false;

        vidasActuales--;
        Debug.Log("¡Golpe recibido! Vidas restantes: " + vidasActuales);

        // Actualizar la barra visual de vidas
        if (barraVidasUI != null)
        {
            barraVidasUI.ActualizarBarra(vidasActuales);
        }

        if (vidasActuales > 0)
        {
            StartCoroutine(SecuenciaRespawn());
        }
        else
        {
            MuerteDefinitiva();
        }
    }

    IEnumerator SecuenciaRespawn()
    {
        // 1. Mostrar sprite de muerte y ocultar los normales
        if (spriteDeath != null) spriteDeath.SetActive(true);
        ActivarSpritesNormales(false);

        // Esperar un momento viendo la animación de muerte
        yield return new WaitForSeconds(0.6f);

        // 2. MOVER AL JUGADOR AL PUNTO DE RESPAWN (-6, 4, 0)
        if (puntoRespawn != null)
        {
            transform.position = puntoRespawn.position;
        }
        else
        {
            Debug.LogWarning("¡Atención! Falta asignar el 'Punto Respawn' en el Inspector del Player.");
        }

        // 3. Ocultar muerte y volver a encender los sprites normales
        if (spriteDeath != null) spriteDeath.SetActive(false);
        ActivarSpritesNormales(true);

        if (miCollider != null) miCollider.enabled = true;

        // 4. Parpadeo de invulnerabilidad
        float tiempoTranscurrido = 0f;
        while (tiempoTranscurrido < tiempoInvulnerabilidad)
        {
            AlternarVisibilidadSprites();
            yield return new WaitForSeconds(0.12f);
            tiempoTranscurrido += 0.12f;
        }

        // 5. Garantizar que queden visibles al terminar
        ActivarSpritesNormales(true);
        esInvulnerable = false;
    }

    private void MuerteDefinitiva()
    {
        if (spriteDeath != null) spriteDeath.SetActive(true);
        ActivarSpritesNormales(false);

        if (miCollider != null) miCollider.enabled = false;

        Debug.Log($"Jugador {numeroJugador} eliminado definitivamente.");
        gameObject.SetActive(false);

        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }

    private void ActivarSpritesNormales(bool estado)
    {
        foreach (GameObject sprite in spritesNormales)
        {
            if (sprite != null) sprite.SetActive(estado);
        }
    }

    private void AlternarVisibilidadSprites()
    {
        foreach (GameObject sprite in spritesNormales)
        {
            if (sprite != null) sprite.SetActive(!sprite.activeSelf);
        }
    }
}