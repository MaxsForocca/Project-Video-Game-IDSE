using UnityEngine;

public class VidaNave : MonoBehaviour
{
    [Header("Salud")]
    public int vidaMaxima = 3;
    private int vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima;
    }

    // Esta función se activa SOLA cuando chocas con algo sólido
    void OnCollisionEnter(Collision colision)
    {
        // 1. Verificamos si chocamos con un obstáculo
        if (colision.gameObject.CompareTag("Obstaculo"))
        {
            RecibirDaño(1);

            // Efecto visual opcional en consola
            Debug.Log("¡CHOQUE! Me queda vida: " + vidaActual);
        }
    }

    internal void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad;

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log("¡GAME OVER!");
        // Aquí podrías reiniciar el nivel o mostrar menú
        // Por ahora, destruimos la nave:
        Destroy(gameObject);
    }
}