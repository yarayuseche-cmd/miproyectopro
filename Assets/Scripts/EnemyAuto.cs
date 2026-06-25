using UnityEngine;

public class EnemyAuto : MonoBehaviour
{
    public float speed = 2f;
    private Vector2 currentDirection;

    void Start() { ChangeDirection(); }

    void Update() { transform.Translate(currentDirection * speed * Time.deltaTime); }

    void ChangeDirection()
    {
        int rand = Random.Range(0, 4);
        Vector2[] dirs = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        currentDirection = dirs[rand];
    }

    // Usamos Collision para que el enemigo rebote contra las paredes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. Si toca una pared, cambia de dirección
        if (collision.gameObject.CompareTag("Wall"))
        {
            ChangeDirection();
        }

        // 2. Si toca al jugador, quita vida
        if (collision.gameObject.CompareTag("Player"))
        {
            // Buscamos el componente correcto: HUDVidasManager
            HUDVidasManager gestorVidas = collision.gameObject.GetComponent<HUDVidasManager>();
            if (gestorVidas != null)
            {
                // NOTA: Asegúrate de tener una función que reste vida aquí
                Debug.Log("¡Contacto con el jugador! Quitando vida...");
            }
        }
    }
}

