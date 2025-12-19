using UnityEngine;

public class VidaNave : MonoBehaviour
{
    [Header("Estadísticas")]
    public float vidaMaxima = 100f;
    public float combustibleMaximo = 100f;
    public float consumoPorSegundo = 0.5f;

    private float vidaActual;
    private float combustibleActual;
    private bool estaMuerto = false;

    void Start()
    {
        
        vidaActual = vidaMaxima;
        combustibleActual = combustibleMaximo;

        
        UIManager.Instance?.ActualizarVida(vidaActual, vidaMaxima);
        UIManager.Instance?.ActualizarCombustible(combustibleActual, combustibleMaximo);
    }

    void Update()
    {
        if (estaMuerto) return;

        // --- LÓGICA DE COMBUSTIBLE ---
        if (combustibleActual > 0)
        {
            // Restamos gasolina según el tiempo
            combustibleActual -= consumoPorSegundo * Time.deltaTime;

            // Evitamos negativos
            if (combustibleActual < 0) combustibleActual = 0;

            // Actualizamos la barra azul
            UIManager.Instance?.ActualizarCombustible(combustibleActual, combustibleMaximo);

            // Si se acaba la gasolina...
            if (combustibleActual <= 0)
            {
                Morir();
            }
        }
    }

    void OnCollisionEnter(Collision colision)
    {
        if (estaMuerto) return;

        if (colision.gameObject.CompareTag("Obstaculo"))
        {
            RecibirDaño(20f);
        }
    }

    public void RecibirDaño(float cantidad)
    {
        vidaActual -= cantidad;
        if (vidaActual < 0) vidaActual = 0;

        // Actualizamos la barra verde
        UIManager.Instance?.ActualizarVida(vidaActual, vidaMaxima);

        Debug.Log("¡GOLPE! Vida restante: " + vidaActual);

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        if (estaMuerto) return; // Evita morir dos veces
        estaMuerto = true;

        Debug.Log("¡GAME OVER!");

        //Desactivamos el script de movimiento para que el dron caiga inerte
        ControladorDron movimiento = GetComponent<ControladorDron>();
        if (movimiento != null)
        {
            movimiento.enabled = false;
        }
        //Destruimos el objeto tras 2 segundos para ver la caída
        Destroy(gameObject, 2f);
    }
}