using UnityEngine;
using System.Collections.Generic;

public class EnemigoPerseguidor : MonoBehaviour
{
    [Header("Movimiento y Persecución")]
    public float velocidadNormal = 2f;
    public float velocidadPersecucion = 3.5f;
    public float distanciaDeteccion = 4f; // Distancia para empezar a perseguir
    public float tiempoMaxPersecucion = 3.5f; // Tiempo que dura persiguiendo (entre 3 y 4 segundos)

    private float timerPersecucion = 0f;
    private bool persiguiendo = false;
    private Transform jugador;
    private Vector2 currentDirection;

    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    [Header("Inteligencia de Caminos (Patrulla)")]
    public float intervaloDecision = 0.5f;
    private float timerDecision = 0f;
    public LayerMask capasObstaculos;

    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.freezeRotation = true;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        // Buscar al jugador automáticamente por su etiqueta
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            jugador = playerObj.transform;
        }

        currentDirection = Vector2.down;
        ActualizarVisualesPorDireccion(currentDirection);
    }

    void FixedUpdate()
    {
        float velocidadActual = velocidadNormal;

        if (jugador != null)
        {
            float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);

            // Si el jugador entra en rango, comienza la persecución
            if (distanciaAlJugador <= distanciaDeteccion)
            {
                persiguiendo = true;
                timerPersecucion = tiempoMaxPersecucion; // Reinicia el tiempo de persecución
            }

            // Lógica de persecución activa
            if (persiguiendo)
            {
                timerPersecucion -= Time.fixedDeltaTime;
                velocidadActual = velocidadPersecucion;

                // Dirección directa hacia el jugador (en ejes principales para mantener el estilo de cuadrícula)
                Vector2 direccionHaciaJugador = (jugador.position - transform.position);
                if (Mathf.Abs(direccionHaciaJugador.x) > Mathf.Abs(direccionHaciaJugador.y))
                {
                    currentDirection = direccionHaciaJugador.x > 0 ? Vector2.right : Vector2.left;
                }
                else
                {
                    currentDirection = direccionHaciaJugador.y > 0 ? Vector2.up : Vector2.down;
                }

                // Si se acaba el tiempo, deja de perseguir y vuelve a patrullar
                if (timerPersecucion <= 0f)
                {
                    persiguiendo = false;
                }
            }
        }

        // Si no está persiguiendo, usa la patrulla normal
        if (!persiguiendo)
        {
            timerDecision += Time.fixedDeltaTime;
            if (timerDecision >= intervaloDecision)
            {
                timerDecision = 0f;
                RevisarNuevosCaminos();
            }
        }

        if (rb != null)
        {
            rb.velocity = currentDirection * velocidadActual;
        }

        ActualizarVisualesPorDireccion(currentDirection);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Destructible"))
        {
            if (!persiguiendo) CambiarDireccionInteligente();
        }

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.name.Contains("Player"))
        {
            AtacarJugador(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si la explosión toca al enemigo, este muere y suma puntos
        if (collision.CompareTag("Explosion"))
        {
            if (PuntajeManager.Instance != null)
            {
                PuntajeManager.Instance.SumarPuntos(100);
            }
            Destroy(gameObject);
        }

        if (collision.CompareTag("Player") || collision.gameObject.name.Contains("Player"))
        {
            AtacarJugador(collision.gameObject);
        }
    }

    void AtacarJugador(GameObject jugadorObj)
    {
        PlayerHealth playerHealth = jugadorObj.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.RecibirDaño();
        }
    }

    void RevisarNuevosCaminos()
    {
        List<Vector2> direccionesLibres = ObtenerDireccionesLibres();
        if (direccionesLibres.Count > 2)
        {
            if (Random.Range(0, 100) < 40)
            {
                int rand = Random.Range(0, direccionesLibres.Count);
                Vector2 nuevaDir = direccionesLibres[rand];
                if (nuevaDir != -currentDirection)
                {
                    currentDirection = nuevaDir;
                }
            }
        }
    }

    void CambiarDireccionInteligente()
    {
        List<Vector2> direccionesLibres = ObtenerDireccionesLibres();
        if (direccionesLibres.Count > 0)
        {
            currentDirection = direccionesLibres[Random.Range(0, direccionesLibres.Count)];
        }
        else
        {
            currentDirection = -currentDirection;
        }
    }

    List<Vector2> ObtenerDireccionesLibres()
    {
        Vector2[] direcciones = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        List<Vector2> libres = new List<Vector2>();

        foreach (Vector2 dir in direcciones)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1.1f, capasObstaculos);
            if (hit.collider == null)
            {
                libres.Add(dir);
            }
        }
        return libres;
    }

    void ActualizarVisualesPorDireccion(Vector2 dir)
    {
        int indiceAnimacion = 1;
        if (dir == Vector2.up) indiceAnimacion = 0;
        if (dir == Vector2.down) indiceAnimacion = 1;
        if (dir == Vector2.left) indiceAnimacion = 2;
        if (dir == Vector2.right) indiceAnimacion = 3;

        ActualizarAnimacion(indiceAnimacion);
    }

    void ActualizarAnimacion(int rand)
    {
        if (anim != null && spriteRenderer != null)
        {
            if (rand == 3)
            {
                spriteRenderer.flipX = true;
                anim.SetInteger("Direction", 2);
            }
            else
            {
                spriteRenderer.flipX = false;
                anim.SetInteger("Direction", rand);
            }
        }
    }
}
