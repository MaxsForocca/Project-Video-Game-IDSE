using UnityEngine;

public class ControlEnemigo : MonoBehaviour
{
    [Header("Configuración de Distancia")]
    public float distanciaParaEmpezar = 15f;
    public float distanciaParaFrenar = 8f;

    [Header("Movimiento Suave")]
    public float velocidad = 4f;
    [Range(0.1f, 5f)] public float velocidadOscilacion = 1.5f; // Más bajo = más lento y suave
    [Range(0.1f, 5f)] public float amplitudOscilacion = 1.5f;  // Más bajo = movimiento más corto

    [Header("Disparo")]
    public GameObject balaPrefab;
    public Transform puntoDisparo;
    public float tiempoEntreDisparos = 1.5f;

    private Transform objetivoJugador;
    private float cronometroDisparo;

    // Variables para la suavidad
    private float alturaBaseY;
    private float anguloSenoidal = 0f; // Nuestro propio reloj para el Seno
    private bool estaActivo = false;

    void Start()
    {
        alturaBaseY = transform.position.y;

        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
        {
            objetivoJugador = jugador.transform;
        }
    }

    void Update()
    {
        if (objetivoJugador == null) return;

        float distancia = Vector3.Distance(transform.position, objetivoJugador.position);

        // 1. Activación
        if (distancia < distanciaParaEmpezar)
        {
            estaActivo = true;
        }

        if (estaActivo)
        {
            // FASE A: Acercarse (Si está lejos)
            if (distancia > distanciaParaFrenar)
            {
                // Se mueve hacia la izquierda
                transform.Translate(Vector3.left * velocidad * Time.deltaTime, Space.World);

                // Mientras se mueve, actualizamos su "Centro" y reseteamos el ángulo
                // Esto es clave: prepara el motor para que cuando frene, empiece suave
                alturaBaseY = transform.position.y;
                anguloSenoidal = 0f;
            }
            // FASE B: Oscilar (Ya llegó)
            else
            {
                // Aumentamos nuestro propio ángulo suavemente
                anguloSenoidal += Time.deltaTime * velocidadOscilacion;

                // Calculamos la nueva Y usando NUESTRO ángulo, no el tiempo global
                // Math.Sin(0) es 0, así que el primer frame el movimiento es 0 (cero saltos)
                float nuevaY = alturaBaseY + Mathf.Sin(anguloSenoidal) * amplitudOscilacion;

                // Aplicamos posición
                transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);
            }

            GestionarDisparo();
        }
    }

    void GestionarDisparo()
    {
        cronometroDisparo += Time.deltaTime;
        if (cronometroDisparo >= tiempoEntreDisparos)
        {
            if (balaPrefab != null && puntoDisparo != null)
            {
                Instantiate(balaPrefab, puntoDisparo.position, Quaternion.identity);
            }
            cronometroDisparo = 0;
        }
    }

    void OnCollisionEnter(Collision colision)
    {
        if (colision.gameObject.CompareTag("Obstaculo"))
        {
            Destroy(gameObject);
        }
    }
}