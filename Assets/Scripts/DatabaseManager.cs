using UnityEngine;
using SQLite;
using System;
using System.Linq;

public class UserData
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    [Unique]
    public string Username { get; set; }
    // Añadimos Email de nuevo para que no marque error
    public string Email { get; set; }
    public string Password { get; set; }

    // Datos de progreso
    public int Puntaje { get; set; }
    public int NivelesPasados { get; set; }
    public string PersonajesDesbloqueados { get; set; }
}

public class DatabaseManager : MonoBehaviour
{
    private SQLiteConnection db;

    void Awake()
    {
        string dbPath = Application.dataPath + "/MiInventario.db";
        db = new SQLiteConnection(dbPath);
        db.CreateTable<UserData>();
    }

    // Ahora este método acepta los 3 parámetros correctamente
    public bool RegistrarUsuario(string user, string email, string pass)
    {
        try
        {
            db.Insert(new UserData
            {
                Username = user,
                Email = email, // Ahora el campo existe en UserData
                Password = pass,
                Puntaje = 0
            });
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Error al registrar: " + e.Message);
            return false;
        }
    }

    public UserData ValidarLogin(string user, string pass)
    {
        return db.Table<UserData>().FirstOrDefault(u => u.Username == user && u.Password == pass);
    }

    public void GuardarProgreso(UserData usuario)
    {
        db.Update(usuario);
    }

    void OnApplicationQuit() { if (db != null) db.Close(); }
}