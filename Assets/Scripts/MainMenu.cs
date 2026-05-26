using UnityEngine;
using UnityEngine.SceneManagement; // 🌟 Obligatorio para poder cambiar de escenas

public class MainMenu : MonoBehaviour
{
    // Este método se llamará al pulsar el botón PLAY
    public void Jugar()
    {
       
        SceneManager.LoadScene("EscenaNiveles");
    }

    
    public void SalirDelJuego()
    {
        Debug.Log("El jugador ha salido del juego."); 
        Application.Quit(); // Cierra el juego (solo funciona en el juego ya exportado .exe, no en el editor)
    }
}
