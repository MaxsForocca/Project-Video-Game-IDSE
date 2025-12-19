using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class BalaTorretaLogica : MonoBehaviour
{
    public int daño = 10;
    private bool impacto;

    void Start()
    {
        Destroy(gameObject, 8f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (impacto) return;

        // 🔥 BUSCAR PlayerSalud EN EL PADRE
        PlayerSalud salud = collision.collider.GetComponentInParent<PlayerSalud>();

        if (salud != null)
        {
            salud.RecibirDaño(daño);
            Debug.Log("BALA TORRETA → VIDA REDUCIDA");

            impacto = true;
            Destroy(gameObject);
            return;
        }

        // Destruir si choca con cualquier cosa que no sea la torreta
        if (!collision.gameObject.CompareTag("Torreta"))
        {
            impacto = true;
            Destroy(gameObject);
        }
    }
}
