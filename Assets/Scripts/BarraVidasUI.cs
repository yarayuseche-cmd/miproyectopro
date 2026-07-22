using UnityEngine;
using UnityEngine.UI;

public class BarraVidasUI : MonoBehaviour
{
    [Header("Referencia a la única imagen de la barra")]
    [SerializeField] private Image imagenBarra; // Arrastra el objeto de UI Image de la barra

    [Header("Tus Tres Sprites de Estado")]
    [SerializeField] private Sprite sprite3Vidas; // Barra con 3 bloques verdes (inicio)
    [SerializeField] private Sprite sprite2Vidas; // Barra con 2 bloques verdes y 1 gris
    [SerializeField] private Sprite sprite1Vida;  // Barra con 1 bloque verde y 2 grises

    // Método principal que se comunica con el PlayerHealth
    public void ActualizarBarra(int vidasActuales)
    {
        if (imagenBarra == null) return;

        // Llamamos a la función encargada de cambiar el sprite según la vida
        CambiarSpriteBarra(vidasActuales);
    }

    // Función independiente que gestiona cuál sprite mostrar
    private void CambiarSpriteBarra(int vidas)
    {
        switch (vidas)
        {
            case 3:
                imagenBarra.sprite = sprite3Vidas;
                break;
            case 2:
                imagenBarra.sprite = sprite2Vidas;
                break;
            case 1:
            case 0: // Si llega a 0 o 1, muestra el sprite de 1 vida restante
                imagenBarra.sprite = sprite1Vida;
                break;
            default:
                Debug.LogWarning("Cantidad de vidas no reconocida para la barra.");
                break;
        }
    }
}