using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float vidaMaxima = 100f;
    public float combustibleMaximo = 100f;

    public float vidaActual;
    public float combustibleActual;

    void Awake()
    {
        vidaActual = vidaMaxima;
        combustibleActual = combustibleMaximo;
    }

    public void RecibirDaño(float daño)
    {
        vidaActual -= daño;
        if (vidaActual <= 0)
            Muerte();
    }

    public void Reparar(float cantidad)
    {
        vidaActual = Mathf.Clamp(vidaActual + cantidad, 0, vidaMaxima);
    }

    public void ConsumirCombustible(float cantidad)
    {
        combustibleActual = Mathf.Clamp(combustibleActual - cantidad, 0, combustibleMaximo);
    }

    public void RecargarCombustible(float cantidad)
    {
        combustibleActual = Mathf.Clamp(combustibleActual + cantidad, 0, combustibleMaximo);
    }

    void Muerte()
    {
        Debug.Log("Jugador destruido");
        Time.timeScale = 0;
    }
}
