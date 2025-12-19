using UnityEngine;

public class BalaPlayerLogica : MonoBehaviour
{
    public int daño = 15;
    private bool impacto;

    void OnTriggerEnter(Collider other)
    {
        if (impacto) return;

        if (other.CompareTag("Torreta"))
        {
            TorretaSalud salud = other.GetComponent<TorretaSalud>();
            if (salud != null)
                salud.RecibirDaño(daño);

            impacto = true;
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Player"))
        {
            impacto = true;
            Destroy(gameObject);
        }
    }
}
