using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Necesario para controlar el Texto de TextMeshPro

public class ControladorPartida : MonoBehaviour
{
    [Header("Configuración de Tiempo")]
    [Tooltip("Tiempo inicial de la partida en segundos (Ej: 120 para 2 min, 180 para 3 min)")]
    public float tiempoRestante = 180f;
    public TextMeshProUGUI textoReloj; // Arrastra aquí tu 'Texto_Tiempo'

    [Header("Control de Jugadores")]
    [Tooltip("Lista con todos los objetos de los jugadores en la escena")]
    public List<GameObject> jugadores;

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
        // Calcula minutos y segundos matemáticamente
        int minutos = Mathf.FloorToInt(tiempoDisplay / 60);
        int segundos = Mathf.FloorToInt(tiempoDisplay % 60);

        // Formatea el texto para que siempre muestre dos dígitos (Ej: 02:04)
        textoReloj.text = string.Format("{0:0}:{1:00}", minutos, segundos);
    }

    void VerificarCondicionesJugadores()
    {
        // Limpiamos de la lista los jugadores que hayan sido destruidos (muertos)
        jugadores.RemoveAll(item => item == null);

        int jugadoresVivos = jugadores.Count;

        // REGLA 1: Todos los jugadores mueren
        if (jugadoresVivos == 0)
        {
            PartidaTerminada("¡EMPATE! Todos los jugadores han muerto.");
        }
        // REGLA 3: Solo queda UN jugador (¡Este jugador ganó!)
        else if (jugadoresVivos == 1)
        {
            PartidaTerminada("¡PARTIDA TERMINADA! Tenemos un ganador definitivo.");
        }
        // REGLA 2: Si quedan 2 o más jugadores, no hace nada, deja que el tiempo siga corriendo.
    }

    void TerminarPartidaPorTiempo()
    {
        partidaTerminada = true;
        Debug.Log("¡Tiempo agotado! Fin de la partida.");
        // Aquí puedes congelar el juego, mostrar pantalla de resultados, etc.
    }

    void PartidaTerminada(string mensajeFin)
    {
        partidaTerminada = true;
        Debug.Log(mensajeFin);
        // Aquí puedes llamar a tu lógica para pausar la acción de los personajes
    }
}