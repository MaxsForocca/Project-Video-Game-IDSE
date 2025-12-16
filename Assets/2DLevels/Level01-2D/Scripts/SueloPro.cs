using UnityEngine;

public class SueloPro : MonoBehaviour
{
    public Transform camara;
    public float longitudDelSuelo = 30f; // ¡MIDE ESTO BIEN!

    // Este valor define cuándo el suelo salta (normalmente es longitud * 1.5)
    private float limiteSalto;

    void Start()
    {
        limiteSalto = longitudDelSuelo;
    }

    void Update()
    {
        // Calculamos la distancia entre este trozo de suelo y la cámara
        float distanciaX = transform.position.x - camara.position.x;

        // CASO 1: Vamos a la Derecha
        // Si el suelo se quedó muy atrás (a la izquierda)...
        if (distanciaX < -longitudDelSuelo * 1.5f)
        {
            // Lo teletransportamos 3 posiciones adelante
            // (Sumamos 3 veces su longitud para ponerlo al frente de la fila)
            transform.position += Vector3.right * (longitudDelSuelo * 3);
        }

        // CASO 2: Vamos a la Izquierda (Retroceso)
        // Si el suelo se quedó muy adelante (a la derecha)...
        else if (distanciaX > longitudDelSuelo * 1.5f)
        {
            // Lo teletransportamos 3 posiciones atrás
            transform.position += Vector3.left * (longitudDelSuelo * 3);
        }
    }
}