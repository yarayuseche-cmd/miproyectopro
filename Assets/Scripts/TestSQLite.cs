using UnityEngine;
using SQLite; // Esto DEBE funcionar si NuGet instaló todo bien

public class TestSQLite : MonoBehaviour
{
    void Awake() // Awake se ejecuta antes que Start
    {
        Debug.Log("--- EL SCRIPT SE ESTÁ EJECUTANDO ---");
    }

    void Start()
    {
        try
        {
            string dbPath = Application.dataPath + "/MiInventario.db";
            Debug.Log("Intentando conectar a: " + dbPath);

            using (var db = new SQLiteConnection(dbPath))
            {
                Debug.Log("¡Conexión establecida con éxito!");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("FALLÓ LA CONEXIÓN: " + e.Message);
        }
    }
}
