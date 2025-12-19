using UnityEngine;

public class BalaEnemiga : MonoBehaviour
{
    public float velocidad = 15f;
    public int daño = 1;

    void Update()
    {
        // Se mueve hacia la IZQUIERDA (hacia el jugador)
        // Usamos Space.World para que siempre vaya a la izquierda de la pantalla
        transform.Translate(Vector3.left * velocidad * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider otro)
    {
        // Si choca con el Jugador (Tu nave)
        if (otro.CompareTag("Player"))
        {
            // Buscamos el script de vida de tu nave y le hacemos daño
            VidaNave vidaDelJugador = otro.GetComponent<VidaNave>();

            if (vidaDelJugador != null)
            {
                vidaDelJugador.RecibirDaño(daño); // Usamos tu función existente
            }

            Destroy(gameObject); // La bala desaparece
        }
    }

    void Start()
    {
        Destroy(gameObject, 5f); // Autodestrucción si no choca con nada
    }
}