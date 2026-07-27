using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class EnemyAuto : MonoBehaviour
{
    public float speed = 2f;
    private Vector2 currentDirection;

    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    [Header("Inteligencia de Caminos")]
    public float intervaloDecision = 0.5f;
    private float timerDecision = 0f;
    public float distanciaDeteccion = 1.1f;

    // MÁSCARA DE CAPA: Para decirle al rayo exactamente qué cosas son obstáculos sólidos
    [Header("Configuración de Colisiones")]
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

        currentDirection = Vector2.down;
        ActualizarVisualesPorDireccion(currentDirection);
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            rb.velocity = currentDirection * speed;
        }

        // Control de IA para pasillos vacíos
        timerDecision += Time.fixedDeltaTime;
        if (timerDecision >= intervaloDecision)
        {
            timerDecision = 0f;
            RevisarNuevosCaminos();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Destructible"))
        {
            CambiarDireccionInteligente();
        }

        // Si toca al jugador por colisión física, le avisamos a su script PlayerHealth
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.name.Contains("Player"))
        {
            AtacarJugador(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Destructible"))
        {
            if (rb != null && rb.velocity.magnitude < 0.1f)
            {
                CambiarDireccionInteligente();
            }
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

        // Si el jugador lo toca mediante un Trigger
        if (collision.CompareTag("Player") || collision.gameObject.name.Contains("Player"))
        {
            AtacarJugador(collision.gameObject);
        }
    }

    // Método centralizado para dañar al jugador usando su propio PlayerHealth
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
                    ActualizarVisualesPorDireccion(currentDirection);
                }
            }
        }
    }

    void CambiarDireccionInteligente()
    {
        List<Vector2> direccionesLibres = ObtenerDireccionesLibres();

        if (direccionesLibres.Count > 0)
        {
            List<Vector2> direccionesFiltradas = new List<Vector2>();
            foreach (Vector2 dir in direccionesLibres)
            {
                if (dir != -currentDirection)
                {
                    direccionesFiltradas.Add(dir);
                }
            }

            if (direccionesFiltradas.Count > 0)
            {
                currentDirection = direccionesFiltradas[Random.Range(0, direccionesFiltradas.Count)];
            }
            else
            {
                currentDirection = direccionesLibres[Random.Range(0, direccionesLibres.Count)];
            }
        }
        else
        {
            currentDirection = -currentDirection;
        }

        ActualizarVisualesPorDireccion(currentDirection);
    }

    List<Vector2> ObtenerDireccionesLibres()
    {
        Vector2[] direcciones = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        List<Vector2> libres = new List<Vector2>();

        foreach (Vector2 dir in direcciones)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distanciaDeteccion, capasObstaculos);

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