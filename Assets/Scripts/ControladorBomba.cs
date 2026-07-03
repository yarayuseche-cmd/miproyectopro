using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ControladorBomba : MonoBehaviour
{
    [Header("Configuración de Bomba")]
    public KeyCode teclaEntrada = KeyCode.Space;
    public GameObject prefabBomba;
    public float tiempoMecha = 3f;
    public int cantidadBombas = 1;
    private int bombasRestantes;

    [Header("Configuración de Explosión")]
    public Explosion prefabExplosion;
    public LayerMask mascaraCapaExplosion; // Configura esto en Unity con la capa "Obstaculos"
    public float duracionExplosion = 1f;
    public int radioExplosion = 3;

    [Header("Objetos Destructibles")]
    public Tilemap mapaTilesDestructibles;
    public Destructible prefabDestructible;

    private void OnEnable()
    {
        bombasRestantes = cantidadBombas;
    }

    private void Update()
    {
        if (bombasRestantes > 0 && Input.GetKeyDown(teclaEntrada))
        {
            StartCoroutine(ColocarBomba());
        }
    }

    private IEnumerator ColocarBomba()
    {
        Vector2 posicion = transform.position;
        posicion.x = Mathf.Round(posicion.x);
        posicion.y = Mathf.Round(posicion.y);

        GameObject bomba = Instantiate(prefabBomba, posicion, Quaternion.identity);
        bombasRestantes--;

        yield return new WaitForSeconds(tiempoMecha);

        posicion = bomba.transform.position;
        posicion.x = Mathf.Round(posicion.x);
        posicion.y = Mathf.Round(posicion.y);

        // Crear la explosión central
        Explosion explosion = Instantiate(prefabExplosion, posicion, Quaternion.identity);
        explosion.ActivarRenderizador(explosion.inicio);
        explosion.DestruirTras(duracionExplosion);

        // Propagar explosión en 4 direcciones
        Explotar(posicion, Vector2.up, radioExplosion);
        Explotar(posicion, Vector2.down, radioExplosion);
        Explotar(posicion, Vector2.left, radioExplosion);
        Explotar(posicion, Vector2.right, radioExplosion);

        Destroy(bomba);
        bombasRestantes++;
    }

    private void Explotar(Vector2 posicion, Vector2 direccion, int longitud)
    {
        if (longitud <= 0) return;

        posicion += direccion;

        // 1. Verificar si hay un bloque destructible en el Tilemap
        Vector3Int celda = mapaTilesDestructibles.WorldToCell(posicion);
        if (mapaTilesDestructibles.HasTile(celda))
        {
            LimpiarDestructible(posicion);
            return; // El fuego se detiene al golpear un bloque destructible
        }

        // 2. Verificar si hay un muro indestructible (usando Raycast contra la capa)
        RaycastHit2D hit = Physics2D.Raycast(posicion, Vector2.zero, 0.1f, mascaraCapaExplosion);
        if (hit.collider != null)
        {
            return; // El fuego se detiene al golpear un muro indestructible
        }

        // 3. Crear el fuego si el camino está libre
        Explosion explosion = Instantiate(prefabExplosion, posicion, Quaternion.identity);
        explosion.ActivarRenderizador(longitud > 1 ? explosion.centro : explosion.fin);
        explosion.EstablecerDireccion(direccion);
        explosion.DestruirTras(duracionExplosion);

        Explotar(posicion, direccion, longitud - 1);
    }

    private void LimpiarDestructible(Vector2 posicion)
    {
        Vector3Int celda = mapaTilesDestructibles.WorldToCell(posicion);
        if (mapaTilesDestructibles.HasTile(celda))
        {
            Instantiate(prefabDestructible, posicion, Quaternion.identity);
            mapaTilesDestructibles.SetTile(celda, null);
        }
    }

    public void AgregarBomba()
    {
        cantidadBombas++;
        bombasRestantes++;
    }
}