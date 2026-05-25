using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Configuración del Jugador")]
    [Range(1, 4)][SerializeField] private int numeroJugador = 1;
    private int vidasActuales = 3;

    [Header("Posición de Reaparición")]
    [SerializeField] private Transform puntoRespawn;

    [Header("Ajustes de Daño")]
    [SerializeField] private float tiempoInvulnerabilidad = 1.5f;

    private HUDVidasManager hudManager;
    private bool esInvulnerable = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D miCollider;

    void Start()
    {
        vidasActuales = 3;
        spriteRenderer = GetComponent<SpriteRenderer>();
        miCollider = GetComponent<Collider2D>();
        hudManager = FindObjectOfType<HUDVidasManager>();

        if (hudManager != null)
        {
            hudManager.ActualizarVidasUI(numeroJugador, vidasActuales);
        }
    }

    // Este método público será llamado por la explosión al tocar al jugador
    public void RecibirDaño()
    {
        if (esInvulnerable) return;

        vidasActuales--;

        if (hudManager != null)
        {
            hudManager.ActualizarVidasUI(numeroJugador, vidasActuales);
        }

        if (vidasActuales > 0)
        {
            StartCoroutine(RespawnCo());
        }
        else
        {
            MuerteDefinitiva();
        }
    }

    private IEnumerator RespawnCo()
    {
        esInvulnerable = true;
        miCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(0.5f); // Pausa antes de reaparecer

        if (puntoRespawn != null)
        {
            transform.position = puntoRespawn.position;
        }

        spriteRenderer.enabled = true;
        miCollider.enabled = true;

        // Efecto visual de parpadeo
        float tiempoPasado = 0;
        while (tiempoPasado < tiempoInvulnerabilidad)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
            tiempoPasado += 0.1f;
        }

        spriteRenderer.enabled = true;
        esInvulnerable = false;
    }

    private void MuerteDefinitiva()
    {
        Debug.Log($"Jugador {numeroJugador} eliminado de la partida.");
        gameObject.SetActive(false);
    }
}
