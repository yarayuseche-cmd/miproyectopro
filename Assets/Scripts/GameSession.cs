using UnityEngine;

// Al ser "static", esta clase no necesita estar en un objeto de la escena.
// Mantiene los datos del usuario logueado disponibles en cualquier lugar del juego.
public static class GameSession
{
    // Esta variable guardará toda la información del usuario (ID, Username, Puntaje, etc.)
    // mientras el juego esté encendido.
    public static UserData UsuarioActual;

    // Método para limpiar la sesión cuando el usuario cierre sesión o salga
    public static void CerrarSesion()
    {
        UsuarioActual = null;
        Debug.Log("Sesión cerrada correctamente.");
    }

    // Método para saber si hay alguien conectado actualmente
    public static bool EstaLogueado()
    {
        return UsuarioActual != null;
    }
}
