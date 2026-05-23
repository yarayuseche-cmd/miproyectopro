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
        renderizadorActivo.estaEnReposo = direccion == Vector2.zero; // 'idle' pertenece a RenderizadorAnimado
    }

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            SecuenciaMuerte();
        }
    }

    private void SecuenciaMuerte()
    {
        enabled = false;
        // Desactiva el script de las bombas usando el nuevo nombre en español
        GetComponent<ControladorBomba>().enabled = false;

        renderizadorArriba.enabled = false;
        renderizadorAbajo.enabled = false;
        renderizadorIzquierda.enabled = false;
        renderizadorDerecha.enabled = false;
        renderizadorMuerte.enabled = true;

        Invoke(nameof(AlTerminarSecuenciaMuerte), 1.25f);
    }

    private void AlTerminarSecuenciaMuerte()
    {
        gameObject.SetActive(false);
        // ¡Listo! Aquí está la conexión con el Gestor de Juego en español
        GameManager.Instancia.ComprobarEstadoVictoria();
    }
}