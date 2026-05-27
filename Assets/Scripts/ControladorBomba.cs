using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ControladorBomba : MonoBehaviour
{
    [Header("Configuración de Bomba")]
    public KeyCode teclaEntrada = KeyCode.LeftShift;
    public GameObject prefabBomba;
    public float tiempoMecha = 3f;
    public int cantidadBombas = 1;
    private int bombasRestantes;

    [Header("Configuración de Explosión")]
    public Explosion prefabExplosion;
    public LayerMask mascaraCapaExplosion;
    public float duracionExplosion = 1f;
    public int radioExplosion = 1;

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

        Explosion explosion = Instantiate(prefabExplosion, posicion, Quaternion.identity);
        explosion.ActivarRenderizador(explosion.inicio); // 'start' suele ser propiedad del script Explosion
        explosion.DestruirTras(duracionExplosion);

        Explotar(posicion, Vector2.up, radioExplosion);
        Explotar(posicion, Vector2.down, radioExplosion);
        Explotar(posicion, Vector2.left, radioExplosion);
        Explotar(posicion, Vector2.right, radioExplosion);

        Destroy(bomba);
        bombasRestantes++;
    }

    private void Explotar(Vector2 posicion, Vector2 direccion, int longitud)
    {
        if (longitud <= 0)
        {
            return;
        }

        posicion += direccion;

        // 1. SI CHOCA CON UN MURO INDESTRUCTIBLE: Frena la explosión de inmediato y no instancia nada
        if (Physics2D.OverlapBox(posicion, Vector2.one / 2f, 0f, LayerMask.GetMask("Indestructibles")))
        {
            return;
        }

        if (Physics2D.OverlapBox(posicion, Vector2.one / 2f, 0f, mascaraCapaExplosion))
        {
            LimpiarDestructible(posicion);
            return;
        }

        
        Explosion explosion = Instantiate(prefabExplosion, posicion, Quaternion.identity);
        explosion.ActivarRenderizador(longitud > 1 ? explosion.centro : explosion.fin);
        explosion.EstablecerDireccion(direccion);
        explosion.DestruirTras(duracionExplosion);

        // Continúa la recursividad
        Explotar(posicion, direccion, longitud - 1);
    }

    private void LimpiarDestructible(Vector2 posicion)
    {
        Vector3Int celda = mapaTilesDestructibles.WorldToCell(posicion);
        TileBase tile = mapaTilesDestructibles.GetTile(celda);

        if (tile != null)
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

    private void OnTriggerExit2D(Collider2D otro)
    {
        if (otro.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            otro.isTrigger = false;
        }
    }
}