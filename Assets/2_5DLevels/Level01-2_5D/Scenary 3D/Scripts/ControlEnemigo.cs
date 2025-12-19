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
    public float velocidadHorizontal = 4f;
    public float velocidadVertical = 3f;
    
    [Tooltip("Pon un número negativo para que vuele más bajo, o positivo para más alto")]
    public float offsetVertical = 0f;
    

    [Header("Disparo")]
    public GameObject balaPrefab;
    public Transform puntoDisparo;
    public float tiempoEntreDisparos = 1.5f;

    private Transform objetivoJugador;
    private float cronometroDisparo;

    private bool estaActivo = false;

    void Start()
    {

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

        if (distancia < distanciaParaEmpezar)
        {
            estaActivo = true;
        }

        if (estaActivo)
        {
            // FASE A: Acercarse
            if (distancia > distanciaParaFrenar)
            {
                transform.Translate(Vector3.left * velocidadHorizontal * Time.deltaTime, Space.World);
            }

            // FASE B: PERSEGUIR VERTICALMENTE
            float objetivoY = objetivoJugador.position.y + offsetVertical;

            float pasoVertical = velocidadVertical * Time.deltaTime;
            float nuevaY = Mathf.MoveTowards(transform.position.y, objetivoY, pasoVertical);

            nuevaY = Mathf.Clamp(nuevaY, sueloY, techoY);

            transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);

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