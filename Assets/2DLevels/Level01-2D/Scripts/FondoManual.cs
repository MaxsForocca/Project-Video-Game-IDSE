using UnityEngine;

public class FondoManual : MonoBehaviour
{
    public GameObject camara;
    public float efectoParallax; // 0.9 para cielo
    public float anchoDeImagen;  // Mide esto bien (ej. 19.2)

    private float startPos;
    private float startZ;

    void Start()
    {
        // TRUCO: Guardamos la posición exacta donde TÚ las pusiste en la escena
        startPos = transform.position.x;
        startZ = transform.position.z;
    }

    void Update()
    {
        // 1. Calculamos el movimiento
        float distanciaRecorrida = (camara.transform.position.x * (1 - efectoParallax));
        float distanciaVisual = (camara.transform.position.x * efectoParallax);

        // 2. Movemos la imagen respetando su posición inicial original (startPos)
        transform.position = new Vector3(startPos + distanciaVisual, transform.position.y, startZ);

        // 3. EL SALTO (Reset)
        // Si la cámara se alejó más que el ancho...
        if (distanciaRecorrida > startPos + anchoDeImagen)
        {
            startPos += anchoDeImagen; // Saltamos hacia adelante
        }
        else if (distanciaRecorrida < startPos - anchoDeImagen)
        {
            startPos -= anchoDeImagen; // Saltamos hacia atrás
        }
    }
}