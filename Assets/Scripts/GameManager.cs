using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instancia { get; private set; }

    [Header("Configuración de Respawn")]
    public Transform puntoDeReinicio;
    public GameObject jugador;
    public float tiempoInvencibilidad = 2f;

    private void Awake()
    {
        if (Instancia != null) { DestroyImmediate(gameObject); }
        else { Instancia = this; }
    }

    // Mantenemos esta función para que ControlarMovimiento deje de dar error
    public void ComprobarEstadoVictoria()
    {
        // Si necesitas lógica de victoria aquí, puedes agregarla
    }

    public void RespawnPlayer()
    {
        StartCoroutine(ProcesoRespawn());
    }

    private IEnumerator ProcesoRespawn()
    {
        jugador.SetActive(false);
        yield return new WaitForSeconds(1f);
        jugador.transform.position = puntoDeReinicio.position;
        jugador.SetActive(true);
        StartCoroutine(EfectoParpadeo());
    }

    private IEnumerator EfectoParpadeo()
    {
        SpriteRenderer sr = jugador.GetComponent<SpriteRenderer>();
        float tiempoFin = Time.time + tiempoInvencibilidad;
        while (Time.time < tiempoFin)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(0.15f);
        }
        sr.enabled = true;
    }
}