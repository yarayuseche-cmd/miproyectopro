using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginUI : MonoBehaviour
{
    public TMP_InputField inputUser;
    public TMP_InputField inputPass;
    public DatabaseManager dbManager;

    public void OnClickLogin()
    {
        UserData usuario = dbManager.ValidarLogin(inputUser.text, inputPass.text);

        if (usuario != null)
        {
            Debug.Log("¡Bienvenido, " + usuario.Username + "! Puntaje: " + usuario.Puntaje);
            // Aquí podrías guardar el ID del usuario en una variable estática para usarla en todo el juego
            SceneManager.LoadScene("MenuPrincipal");
        }
        else
        {
            Debug.LogError("Usuario o contraseña incorrectos.");
        }
    }
}