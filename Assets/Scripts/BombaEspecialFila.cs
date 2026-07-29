using UnityEngine;
using UnityEngine.SceneManagement;

public class BombaEspecialFila : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private string nombreEscenaNivel2 = "Bomberman 2"; // Cambia por el nombre de tu escena
    public LayerMask capasA_Destruir;
    private bool yaFueUsada = false;

    void Start()
    {
        // Si no estamos en el nivel 2, desactivamos o destruimos este objeto especial
        if (SceneManager.GetActiveScene().name != nombreEscenaNivel2)
        {
            gameObject.SetActive(false);
        }
    }

    // Método para activar la bomba de impacto en fila (llámalo al colocar la bomba)
    public void ActivarBombaEspecial()
    {
        if (yaFueUsada) return;

        yaFueUsada = true;
        ExplotarFilaCompleta();
    }

    void ExplotarFilaCompleta()
    {
        // Lanza un raycast horizontal largo para destruir toda la fila en el eje X
        Vector2 posicionOrigen = transform.position;

        // Destruir hacia la derecha
        RaycastHit2D[] hitsDerecha = Physics2D.RaycastAll(posicionOrigen, Vector2.right, 15f, capasA_Destruir);
        foreach (var hit in hitsDerecha)
        {
            DestruirObjetoFila(hit.collider.gameObject);
        }

        // Destruir hacia la izquierda
        RaycastHit2D[] hitsIzquierda = Physics2D.RaycastAll(posicionOrigen, Vector2.left, 15f, capasA_Destruir);
        foreach (var hit in hitsIzquierda)
        {
            DestruirObjetoFila(hit.collider.gameObject);
        }

        // Eliminar el objeto de la bomba roja tras su uso único
        Destroy(gameObject, 0.2f);
    }

    void DestruirObjetoFila(GameObject obj)
    {
        if (obj.CompareTag("Destructible") || obj.CompareTag("Enemy"))
        {
            Destroy(obj);
        }
    }
}
