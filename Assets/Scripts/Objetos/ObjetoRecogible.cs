using UnityEngine;

public class ObjetoRecogible : MonoBehaviour
{
    public enum Tipo { Moneda, Combustible, Vida, Chatarra }
    public Tipo tipo;
    public float cantidad = 10f;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerStats stats = other.GetComponent<PlayerStats>();

        switch (tipo)
        {
            case Tipo.Combustible:
                stats.RecargarCombustible(cantidad);
                break;
            case Tipo.Vida:
                stats.Reparar(cantidad);
                break;
        }

        Destroy(gameObject);
    }
}
