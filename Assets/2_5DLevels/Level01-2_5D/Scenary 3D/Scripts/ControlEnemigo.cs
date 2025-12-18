using UnityEngine;

public class ControlEnemigo : MonoBehaviour
{
    [Header("Configuración de Distancia")]
    public float distanciaParaEmpezar = 15f;
    public float distanciaParaFrenar = 8f;

    [Header("Límites de Pantalla")]
    public float techoY = 4.5f;
    public float sueloY = -4.5f;

    [Header("Movimiento de Seguimiento")]
    public float velocidadHorizontal = 4f; // (Antes llamada 'velocidad')
    // NUEVO: Velocidad para subir/bajar persiguiendo al jugador
    public float velocidadVertical = 3f;
    // --- NUEVO: AJUSTE MANUAL ---
    [Tooltip("Pon un número negativo para que vuele más bajo, o positivo para más alto")]
    public float offsetVertical = 0f;
    // ----------------------------

    [Header("Disparo")]
    public GameObject balaPrefab;
    public Transform puntoDisparo;
    public float tiempoEntreDisparos = 1.5f;

    private Transform objetivoJugador;
    private float cronometroDisparo;

    // variables para la suavidad (Ya no se usan)
    // private float alturaBaseY;
    // private float anguloSenoidal = 0f;
    private bool estaActivo = false;

    void Start()
    {
        // alturaBaseY = transform.position.y; // Ya no hace falta

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
            // FASE A: Acercarse (Moverse a la izquierda)
            if (distancia > distanciaParaFrenar)
            {
                // Usamos la nueva variable 'velocidadHorizontal'
                transform.Translate(Vector3.left * velocidadHorizontal * Time.deltaTime, Space.World);
            }

            // FASE B: PERSEGUIR VERTICALMENTE (El cambio importante está aquí)
            // Ya no usamos 'else', queremos que persiga SIEMPRE que esté activo

            // 1. ¿A qué altura está el jugador ahora mismo?
            float objetivoY = objetivoJugador.position.y + offsetVertical;

            // 2. Nos movemos suavemente hacia esa meta
            float pasoVertical = velocidadVertical * Time.deltaTime;
            float nuevaY = Mathf.MoveTowards(transform.position.y, objetivoY, pasoVertical);

            // 3. Respetamos los límites (¡Revisa que sueloY sea lo bastante bajo!)
            nuevaY = Mathf.Clamp(nuevaY, sueloY, techoY);

            transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);

            // El disparo sigue igual
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