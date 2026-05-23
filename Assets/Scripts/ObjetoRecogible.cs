using UnityEngine;

public class ObjetoRecogible : MonoBehaviour
{
    public enum TipoItem
    {
        ExtraBomba,
        ExtraExplosion,
        ExtraVelocidad
    }

    [Header("Configuración del Item")]
    public TipoItem tipo = TipoItem.ExtraBomba;

    private void Recoger(GameObject jugador)
    {
        switch (tipo)
        {
            case TipoItem.ExtraBomba:
                // Llama al método que tradujimos antes en el script del jugador
                jugador.GetComponent<ControladorBomba>().AgregarBomba();
                break;

            case TipoItem.ExtraExplosion:
                // Aumenta el radio de explosión directamente
                jugador.GetComponent<ControladorBomba>().radioExplosion++;
                break;

            case TipoItem.ExtraVelocidad:
                // OJO: Aquí asumo que tu script de movimiento se llamará "MovimientoJugador" 
                // y tiene una variable llamada "velocidad". 
                // Si no es así, habrá que ajustar el nombre abajo:
                // jugador.GetComponent<MovimientoJugador>().velocidad++;
                break;
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player")) // "Player" suele ser el tag por defecto en Unity
        {
            Recoger(otro.gameObject);
        }
    }
}
