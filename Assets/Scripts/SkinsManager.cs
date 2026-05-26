using UnityEngine;
using UnityEngine.UI;

public class SkinsManager : MonoBehaviour
{
    [Header("Configuración de Skins")]
    [Tooltip("El ID de la skin seleccionada por defecto si el jugador nunca ha elegido una.")]
    public int skinPorDefecto = 0;

    private const string SkinKey = "SkinSeleccionada";

    /// <param name="skinID">ID numérico de la skin (Ej: 0 = Clásico, 1 = Azul, etc.)</param>
    public void SeleccionarSkin(int skinID)
    {
        PlayerPrefs.SetInt(SkinKey, skinID);
        PlayerPrefs.Save(); // Asegura que el dato se guarde inmediatamente en el disco

        Debug.Log($"<color=cyan>SkinsManager:</color> ¡Skin {skinID} guardada con éxito!");
    }

    
    public static int ObtenerSkinActiva()
    {
        // Si no existe la llave, devolvemos 0 (la skin por defecto)
        return PlayerPrefs.GetInt(SkinKey, 0);
    }
}
