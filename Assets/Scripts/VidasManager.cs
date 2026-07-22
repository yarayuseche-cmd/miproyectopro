using UnityEngine;
using TMPro;

public class HUDVidasManager : MonoBehaviour
{
    [Header("Referencias de UI (P1 al P4)")]
    [SerializeField] private TMP_Text[] textosVidas;

    private int[] vidasJugadores = { 3, 3, 3, 3 };

    public void RestarVida(int numeroJugador)
    {
        int index = numeroJugador - 1;
        if (index >= 0 && index < vidasJugadores.Length)
        {
            vidasJugadores[index]--;
            ActualizarVidasUI(numeroJugador, vidasJugadores[index]);

            if (vidasJugadores[index] > 0)
            {
                GameManager.Instancia.RespawnPlayer();
            }
        }
    }

    public void ActualizarVidasUI(int numeroJugador, int vidasRestantes)
    {
        int index = numeroJugador - 1;
        if (index >= 0 && index < textosVidas.Length)
        {
            if (vidasRestantes > 0) textosVidas[index].text = "x" + vidasRestantes;
            else { textosVidas[index].text = "ELIMINADO"; textosVidas[index].color = Color.red; }
        }
    }
}