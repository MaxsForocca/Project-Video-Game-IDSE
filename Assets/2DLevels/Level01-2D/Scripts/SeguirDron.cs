using UnityEngine;

public class SeguirDron : MonoBehaviour
{
    public Transform objetivo;
    public float suavizado = 5f;

    // Guardamos la altura fija (Y) que tenía la cámara al empezar
    private float alturaFija;
    private float distanciaZ;
    private float distanciaX;

    void Start()
    {
        // Memorizamos la altura original y la distancia Z
        alturaFija = transform.position.y;
        distanciaZ = transform.position.z;

        // Calculamos la distancia horizontal inicial con el dron
        if (objetivo != null)
            distanciaX = transform.position.x - objetivo.position.x;
    }

    void LateUpdate()
    {
        if (objetivo != null)
        {
            // Solo calculamos el destino en el eje X (Horizontal)
            // Mantenemos la 'alturaFija' y la 'distanciaZ' originales
            Vector3 destino = new Vector3(objetivo.position.x + distanciaX, alturaFija, distanciaZ);

            // Movemos la cámara suavemente
            transform.position = Vector3.Lerp(transform.position, destino, suavizado * Time.deltaTime);
        }
    }
}