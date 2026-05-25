using UnityEngine;
using TMPro;

public class HUDVidasManager : MonoBehaviour
{
    [Header("Referencias de UI (P1 al P4)")]
    [SerializeField] private TMP_Text[] textosVidas;

    public void ActualizarVidasUI(int numeroJugador, int vidasRestantes)
    {
        int index = numeroJugador - 1; // Ajustamos al índice del array (0 a 3)

        if (index >= 0 && index < textosVidas.Length)
        {
            if (vidasRestantes > 0)
            {
                textosVidas[index].text = "x" + vidasRestantes;
            }
            else
            {
                textosVidas[index].text = "ELIMINADO";
                textosVidas[index].color = Color.red;
            }
        }
    }
}
