using UnityEngine;
using TMPro;

public class RegisterUI : MonoBehaviour
{
    [Header("Referencias de UI")]
    public TMP_InputField inputUser;
    public TMP_InputField inputEmail;
    public TMP_InputField inputPass;

    [Header("Referencias de Sistema")]
    public DatabaseManager dbManager;

    public void OnClickRegistrar()
    {
        // 1. Verificación de seguridad (Evita NullReferenceException)
        if (dbManager == null)
        {
            Debug.LogError("¡Error! El DatabaseManager no está asignado en el Inspector de RegisterUI.");
            return;
        }

        if (inputUser == null || inputEmail == null || inputPass == null)
        {
            Debug.LogError("¡Error! Uno de los campos de texto (InputField) no está asignado.");
            return;
        }

        // 2. Validación básica de campos vacíos
        if (string.IsNullOrEmpty(inputUser.text) || string.IsNullOrEmpty(inputPass.text))
        {
            Debug.LogWarning("Por favor, rellena todos los campos.");
            return;
        }

        // 3. Llamada a la base de datos
        // Asegúrate de que este método exista en tu DatabaseManager.cs
        dbManager.RegistrarUsuario(inputUser.text, inputEmail.text, inputPass.text);

        Debug.Log("Registro intentado para: " + inputUser.text);
    }
}