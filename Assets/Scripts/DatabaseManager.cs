using UnityEngine;
using SQLite;
using System.Linq;

public class UserData
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    [Unique]
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PreguntaSecreta { get; set; }
    public string RespuestaSecreta { get; set; }
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

    public bool RegistrarUsuario(string user, string email, string pass, string pregunta, string respuesta)
    {
        try
        {
            db.Insert(new UserData { Username = user, Email = email, Password = pass, PreguntaSecreta = pregunta, RespuestaSecreta = respuesta });
            return true;
        }
        catch { return false; }
    }

    public UserData ValidarLogin(string user, string pass)
    {
        return db.Table<UserData>().FirstOrDefault(u => u.Username == user && u.Password == pass);
    }

    public UserData BuscarUsuarioPorNombre(string user)
    {
        return db.Table<UserData>().FirstOrDefault(u => u.Username == user);
    }

    public void GuardarProgreso(UserData usuario)
    {
        db.Update(usuario);
    }

    public void RegistrarUsuario(string user, string email, string pass)
    {
        UserData nuevoUsuario = new UserData { Username = user, Email = email, Password = pass };
        db.Insert(nuevoUsuario);
        Debug.Log("Usuario guardado en la base de datos.");
    }
}