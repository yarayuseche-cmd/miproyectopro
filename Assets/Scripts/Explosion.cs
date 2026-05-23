using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Header("Renderizadores de Animación")]
    public RenderizadorAnimado inicio;
    public RenderizadorAnimado centro;
    public RenderizadorAnimado fin;

    [Header("Ajustes de Colisión del Fuego")]
   
    public LayerMask capasBloqueo;

    private void Awake()
    {
        
        Collider2D choque = Physics2D.OverlapCircle(transform.position, 0.1f, capasBloqueo);

        if (choque != null)
        {
           
            Destroy(gameObject);
        }
    }

    public void ActivarRenderizador(RenderizadorAnimado renderizadorElegido)
    {
        // Activa solo el renderizador que coincida con el pasado por parámetro
        inicio.enabled = renderizadorElegido == inicio;
        centro.enabled = renderizadorElegido == centro;
        fin.enabled = renderizadorElegido == fin;
    }

    public void EstablecerDireccion(Vector2 direccion)
    {
        // Calcula el ángulo basado en la dirección (arriba, abajo, izquierda, derecha)
        float angulo = Mathf.Atan2(direccion.y, direccion.x);

        // Aplica la rotación en el eje Z (forward)
        transform.rotation = Quaternion.AngleAxis(angulo * Mathf.Rad2Deg, Vector3.forward);
    }

    public void DestruirTras(float segundos)
    {
        // Elimina el objeto de la explosión después de un tiempo determinado
        Destroy(gameObject, segundos);
    }
}