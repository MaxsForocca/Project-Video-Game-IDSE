using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidad = 20f;
    public int daño = 1;

    void Update()
    {
        transform.Translate(Vector3.right * velocidad * Time.deltaTime, Space.World);
    }

    // AÑADE ESTO:
    void OnTriggerEnter(Collider otro)
    {
        // Si choco contra un Enemigo...
        if (otro.CompareTag("Enemigo"))
        {
            // Busco su script de vida y le hago daño
            VidaEnemigo salud = otro.GetComponent<VidaEnemigo>();
            if (salud != null)
            {
                salud.RecibirDaño(daño);
            }
            // Destruyo la bala para que no lo atraviese
            Destroy(gameObject);
        }
        // Si choco contra un obstáculo (rocas), solo se destruye la bala
        else if (otro.CompareTag("Obstaculo"))
        {
            Destroy(gameObject);
        }
    }
}