using UnityEngine;

public class LineCollider : MonoBehaviour
{
    public float damage = 10f;      // Daño que hace el rayo
    private Vector3 startPos;       // Posición de inicio de la línea
    private Vector3 endPos;         // Posición final de la línea
    private Collider lineCollider;  // Collider del rayo

    void Start()
    {
        lineCollider = gameObject.AddComponent<BoxCollider>();
        lineCollider.isTrigger = true;
    }

    // Método para configurar la línea
    public void SetUp(Vector3 start, Vector3 direction, float distance)
    {
        startPos = start;
        endPos = start + direction * distance;

        // Ajustar el BoxCollider para que se ajuste a la longitud de la línea
        Vector3 size = new Vector3(0.1f, 0.1f, Vector3.Distance(start, endPos));
        lineCollider.bounds.SetMinMax(startPos, endPos);
    }

    void OnTriggerEnter(Collider other)
    {
        // Si el rayo golpea a un objeto con el tag "Enemigo", aplicar daño
        if (other.CompareTag("Enemigo"))
        {
            // Aplicar daño (deberías tener un script de salud en el enemigo)
            other.GetComponent<Enemigo>().RecibirDaño(damage);

            // También podemos destruir la línea si impacta
            Destroy(gameObject);
        }
    }
}
