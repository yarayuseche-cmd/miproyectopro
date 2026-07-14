using UnityEngine;
using SQLite;
using System;

public class UserData
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    [Unique]
    public string Username { get; set; }
    public string Password { get; set; }

    // Datos de progreso
    public int Puntaje { get; set; }
    public int NivelesPasados { get; set; }
    public string PersonajesDesbloqueados { get; set; }
}

public class DatabaseManager : MonoBehaviour
{
    private SQLiteConnection db;

    void Start()
    {
        string dbPath = Application.dataPath + "/MiInventario.db";
        db = new SQLiteConnection(dbPath);
        db.CreateTable<UserData>();
    }

    // Registrar
    public bool RegistrarUsuario(string user, string email, string pass)
    {
        try
        {
            db.Insert(new UserData { Username = user, Password = pass });
            return true;
        }
        catch { return false; }
    }

    // Login
    public UserData ValidarLogin(string user, string pass)
    {
        return db.Table<UserData>().FirstOrDefault(u => u.Username == user && u.Password == pass);
    }

    // Guardar progreso
    public void GuardarProgreso(UserData usuario)
    {
        db.Update(usuario);
    }
}