using UnityEngine;

public class PlayerSalud : MonoBehaviour
{
    [Header("Salud")]
    public int saludMaxima = 100;
    public int saludActual;

    private bool estaVivo = true;

    // Referencia al controlador de movimiento para resetear posición
    private PlayerMovimiento_nivel6 movimientoScript;

    void Start()
    {
        saludActual = saludMaxima;
        movimientoScript = GetComponent<PlayerMovimiento_nivel6>();
    }

    public void RecibirDaño(int cantidad)
    {
        if (!estaVivo) return;

        saludActual -= cantidad;
        Debug.Log($"Player recibió {cantidad}. Vida: {saludActual}");

        if (saludActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        estaVivo = false;
        Debug.Log("PLAYER MUERTO");

        // Respawn después de 2 segundos
        if (movimientoScript != null)
        {
            Invoke(nameof(Respawn), 2f);
        }
    }

    void Respawn()
    {
        saludActual = saludMaxima;
        estaVivo = true;

        if (movimientoScript != null)
            movimientoScript.RespawnPlayer();

        Debug.Log("PLAYER REGENERADO EN EL PUNTO DE INICIO");
    }


    public bool EstaVivo()
    {
        return estaVivo;
    }
}
