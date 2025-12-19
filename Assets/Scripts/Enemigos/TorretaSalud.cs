using UnityEngine;

public class TorretaSalud : MonoBehaviour
{
    public int saludMaxima = 100;
    public int saludActual;

    private bool viva = true;

    void Start()
    {
        saludActual = saludMaxima;
    }

    public void RecibirDaño(int daño)
    {
        if (!viva) return;

        saludActual -= daño;
        Debug.Log($"Torreta recibió {daño}. Vida: {saludActual}");

        if (saludActual <= 0)
            Morir();
    }

    void Morir()
    {
        viva = false;
        Debug.Log("TORRETA DESTRUIDA");
        Destroy(gameObject);
    }
}
