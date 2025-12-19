using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidad = 20f;
    public float vida = 5f; // se autodestruye después de X segundos
    public int daño = 10;

    private Transform objetivo;

    public void Inicializar(Transform target)
    {
        objetivo = target;
        Destroy(gameObject, vida);
    }

    void Update()
    {
        if (objetivo != null)
        {
            // Mueve la bala hacia el jugador
            Vector3 direccion = (objetivo.position - transform.position).normalized;
            transform.position += direccion * velocidad * Time.deltaTime;

            // Rotar la bala hacia el jugador
            transform.rotation = Quaternion.LookRotation(direccion);
        }
        else
        {
            Destroy(gameObject); // si el objetivo desaparece
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Aquí aplicarías daño al jugador
            // ejemplo: other.GetComponent<PlayerHealth>().RecibirDaño(daño);

            Destroy(gameObject);
        }
    }
}
