using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControladorPartida : MonoBehaviour
{
    [Header("Configuración de Tiempo")]
    public float tiempoRestante = 180f;
    public TextMeshProUGUI textoReloj;

    [Header("Control de Jugadores")]
    public List<GameObject> jugadores;

    [Header("UI de Vidas de Jugadores")]
    public TextMeshProUGUI textoVidaP1;
    public TextMeshProUGUI textoVidaP2;
    public TextMeshProUGUI textoVidaP3;
    public TextMeshProUGUI textoVidaP4;

    private bool partidaTerminada = false;

    void Update()
    {
        if (partidaTerminada) return;

        ControlarTiempo();
        VerificarCondicionesJugadores();
    }

    void ControlarTiempo()
    {
        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            ActualizarInterfazReloj(tiempoRestante);
        }
        else
        {
            tiempoRestante = 0;
            ActualizarInterfazReloj(tiempoRestante);
            TerminarPartidaPorTiempo();
        }
    }

    void ActualizarInterfazReloj(float tiempoDisplay)
    {
        int minutes = Mathf.FloorToInt(tiempoDisplay / 60);
        int segundos = Mathf.FloorToInt(tiempoDisplay % 60);
        textoReloj.text = string.Format("{0:0}:{1:00}", minutes, segundos);
    }

    void VerificarCondicionesJugadores()
    {
        int jugadoresVivos = 0;

        foreach (GameObject jugador in jugadores)
        {
            // El jugador cuenta como vivo si su objeto está encendido en la jerarquía
            if (jugador != null && jugador.activeSelf)
            {
                jugadoresVivos++;
            }
        }

        if (jugadoresVivos == 0)
        {
            PartidaTerminada("¡EMPATE! Todos los jugadores han muerto.");
        }
        else if (jugadoresVivos == 1 && jugadores.Count > 1)
        {
            PartidaTerminada("¡PARTIDA TERMINADA! Tenemos un ganador definitivo.");
        }
    }

    // Este método solo se encarga de pintar los números en tus nubes de la UI
    public void ActualizarVidaInterfaz(int numeroJugador, int vidasRestantes)
    {
        int vidasMostrar = Mathf.Max(0, vidasRestantes);

        if (numeroJugador == 1 && textoVidaP1 != null) textoVidaP1.text = vidasMostrar.ToString() + "X";
        if (numeroJugador == 2 && textoVidaP2 != null) textoVidaP2.text = vidasMostrar.ToString() + "X";
        if (numeroJugador == 3 && textoVidaP3 != null) textoVidaP3.text = vidasMostrar.ToString() + "X";
        if (numeroJugador == 4 && textoVidaP4 != null) textoVidaP4.text = vidasMostrar.ToString() + "X";
    }

    void TerminarPartidaPorTiempo()
    {
        partidaTerminada = true;
        Debug.Log("¡Tiempo finalizado!");
    }

    void PartidaTerminada(string mensajeFin)
    {
        partidaTerminada = true;
        Debug.Log(mensajeFin);
    }
}