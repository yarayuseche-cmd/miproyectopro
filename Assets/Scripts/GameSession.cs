using UnityEngine;

public static class GameSession
{
    public static UserData UsuarioActual;

    public static void CerrarSesion()
    {
        UsuarioActual = null;
    }

    public static bool EstaLogueado()
    {
        return UsuarioActual != null;
    }
}
