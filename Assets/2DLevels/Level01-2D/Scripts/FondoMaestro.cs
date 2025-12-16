using UnityEngine;

public class FondoMaestro : MonoBehaviour
{
    [Header("Configuración")]
    public Transform camara;
    public float efectoParallax; // 0.9 = Muy lejos (cielo), 0.5 = Medio
    public float anchoImagen;    // TU NÚMERO MÁGICO (ej. 19.2)

    [Header("Arrastra aquí tus 3 imágenes")]
    public Transform imagenIzquierda;
    public Transform imagenCentro;
    public Transform imagenDerecha;

    void Update()
    {
        // 1. Calculamos dónde está el "foco" del fondo basado en la cámara
        // Esto determina en qué "casilla" del mundo infinito estamos
        float distanciaRecorrida = camara.position.x * (1 - efectoParallax);

        // 2. Calculamos la posición central base (Grid Snapping)
        // Redondeamos para saber cuál es el centro actual más cercano
        float centroActual = Mathf.Round(distanciaRecorrida / anchoImagen) * anchoImagen;

        // 3. Calculamos el desplazamiento visual (El efecto Parallax)
        float desplazamiento = camara.position.x * efectoParallax;

        // 4. POSICIONAMIENTO FORZOSO (Aquí está la magia)
        // Obligamos a las imágenes a colocarse relativas a ese centro calculado.
        // Nunca se pueden montar porque sumamos/restamos el ancho matemáticamente.

        // Centro: Se coloca en el centro calculado + el movimiento parallax
        imagenCentro.position = new Vector3(centroActual + desplazamiento, imagenCentro.position.y, imagenCentro.position.z);

        // Izquierda: Siempre exactamente un ancho a la izquierda del centro
        imagenIzquierda.position = new Vector3(centroActual - anchoImagen + desplazamiento, imagenIzquierda.position.y, imagenIzquierda.position.z);

        // Derecha: Siempre exactamente un ancho a la derecha del centro
        imagenDerecha.position = new Vector3(centroActual + anchoImagen + desplazamiento, imagenDerecha.position.y, imagenDerecha.position.z);
    }
}