using UnityEngine;
using TMPro;

public class RegisterUI : MonoBehaviour
{
    public TMP_InputField inputUsername, inputEmail, inputPassword, inputPregunta, inputRespuesta;
    public DatabaseManager dbManager;

    public void OnClickRegistrar()
    {
        if (dbManager == null) return;

        bool exito = dbManager.RegistrarUsuario(
            inputUsername.text,
            inputEmail.text,
            inputPassword.text,
            inputPregunta.text,
            inputRespuesta.text
        );

        if (exito) Debug.Log("Registro exitoso.");
        else Debug.LogError("Error en registro.");
    }
}
