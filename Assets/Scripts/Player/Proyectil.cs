using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float tiempoVida = 3f;

    void Start()
    {
        Destroy(gameObject, tiempoVida);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemigo"))
        {
            Destroy(collision.gameObject); // prueba
        }

        Destroy(gameObject);
    }
}
