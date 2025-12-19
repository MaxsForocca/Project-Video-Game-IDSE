using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float salud = 100f;  // Salud del enemigo

    // Método para recibir daño
    public void RecibirDaño(float cantidad)
    {
        salud -= cantidad;

        if (salud <= 0f)
        {
            Muerte();
        }
    }

    // Método para destruir al enemigo cuando muere
    void Muerte()
    {
        // Aquí puedes agregar una animación, efectos, etc.
        Destroy(gameObject);
    }
}
