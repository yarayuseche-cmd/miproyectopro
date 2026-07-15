using UnityEngine;
using TMPro;

public class RecoveryUI : MonoBehaviour
{
    public TMP_InputField inputUser, inputNuevaPass, inputConfirmarPass;
    public DatabaseManager dbManager;
    private UserData usuarioEncontrado;

    public void OnClickBuscar()
    {
        if (dbManager == null) return;
        usuarioEncontrado = dbManager.BuscarUsuarioPorNombre(inputUser.text);

        if (usuarioEncontrado != null) Debug.Log("Usuario encontrado.");
        else Debug.LogError("Usuario no encontrado.");
    }

    public void OnClickConfirmarCambio()
    {
        if (usuarioEncontrado != null && inputNuevaPass.text == inputConfirmarPass.text && !string.IsNullOrEmpty(inputNuevaPass.text))
        {
            usuarioEncontrado.Password = inputNuevaPass.text;
            dbManager.GuardarProgreso(usuarioEncontrado);
            Debug.Log("Contraseña actualizada correctamente.");
        }
        else Debug.LogError("Datos incorrectos o no se ha buscado el usuario.");
    }
}