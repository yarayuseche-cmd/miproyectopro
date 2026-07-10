using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ControladorMovimiento : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direccion = Vector2.down;
    public float velocidad = 5f;

    [Header("Teclas de Entrada")]
    public KeyCode teclaArriba = KeyCode.W;
    public KeyCode teclaAbajo = KeyCode.S;
    public KeyCode teclaIzquierda = KeyCode.A;
    public KeyCode teclaDerecha = KeyCode.D;

    [Header("Renderizadores de Sprites")]
    public RenderizadorAnimado renderizadorArriba;
    public RenderizadorAnimado renderizadorAbajo;
    public RenderizadorAnimado renderizadorIzquierda;
    public RenderizadorAnimado renderizadorDerecha;
    public RenderizadorAnimado renderizadorMuerte;
    private RenderizadorAnimado renderizadorActivo;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        renderizadorActivo = renderizadorAbajo;
    }

    private void Update()
    {
        if (Input.GetKey(teclaArriba))
        {
            EstablecerDireccion(Vector2.up, renderizadorArriba);
        }
        else if (Input.GetKey(teclaAbajo))
        {
            EstablecerDireccion(Vector2.down, renderizadorAbajo);
        }
        else if (Input.GetKey(teclaIzquierda))
        {
            EstablecerDireccion(Vector2.left, renderizadorIzquierda);
        }
        else if (Input.GetKey(teclaDerecha))
        {
            EstablecerDireccion(Vector2.right, renderizadorDerecha);
        }
        else
        {
            EstablecerDireccion(Vector2.zero, renderizadorActivo);
        }
    }

    private void FixedUpdate()
    {
        Vector2 posicion = rb.position;
        Vector2 traslacion = velocidad * Time.fixedDeltaTime * direccion;
        rb.MovePosition(posicion + traslacion);
    }

    private void EstablecerDireccion(Vector2 nuevaDireccion, RenderizadorAnimado renderizadorSprite)
    {
        direccion = nuevaDireccion;
        renderizadorArriba.enabled = renderizadorSprite == renderizadorArriba;
        renderizadorAbajo.enabled = renderizadorSprite == renderizadorAbajo;
        renderizadorIzquierda.enabled = renderizadorSprite == renderizadorIzquierda;
        renderizadorDerecha.enabled = renderizadorSprite == renderizadorDerecha;

        renderizadorActivo = renderizadorSprite;
        renderizadorActivo.estaEnReposo = direccion == Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D otro)
    {
        Debug.Log("Trigger detectado con: " + otro.gameObject.name);
        if (otro.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            SecuenciaMuerte();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisión detectada con: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            SecuenciaMuerte();
        }
    }

    // Función pública para que otros scripts (como el enemigo) puedan llamarla
    public void SecuenciaMuerte()
    {
        if (!enabled) return; // Evita que se ejecute dos veces

        enabled = false;
        GetComponent<ControladorBomba>().enabled = false;

        renderizadorArriba.enabled = false;
        renderizadorAbajo.enabled = false;
        renderizadorIzquierda.enabled = false;
        renderizadorDerecha.enabled = false;

        renderizadorMuerte.enabled = true;
        renderizadorMuerte.ReproducirAnimacionMuerte();

        Invoke(nameof(AlTerminarSecuenciaMuerte), renderizadorMuerte.ObtenerTiempoAnimacionMuerte());
    }

    private void AlTerminarSecuenciaMuerte()
    {
        gameObject.SetActive(false);
        GameManager.Instancia.ComprobarEstadoVictoria();
    }
}