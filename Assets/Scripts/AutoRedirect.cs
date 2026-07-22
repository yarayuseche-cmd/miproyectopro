using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoRedirect : MonoBehaviour
{
    void Start()
    {
        if (SessionManager.Instance != null && !string.IsNullOrEmpty(SessionManager.Instance.usuarioLogueado))
        {
            SceneManager.LoadScene("MenuPrincipal");
        }
    }
}