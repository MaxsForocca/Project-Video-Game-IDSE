using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerSalud))]
public class PlayerMovimiento_nivel6 : MonoBehaviour
{
    [Header("Respawn")]
    public Transform puntoDeInicio; // Punto de inicio para respawn

    [Header("Movimiento")]
    public float velocidadMaxima = 30f;       // Velocidad hacia adelante/atrás
    public float aceleracion = 6f;            // Aceleración controlable
    public float friccion = 5f;
    public float velocidadGiro = 70f;         // Giro controlable
    public float velocidadVertical = 15f;     // Fuerza vertical para subir/bajar
    public float suavizadoVertical = 3f;      // Suavizado del movimiento vertical

    [Header("Inclinación visual")]
    public Transform modeloNave;
    public float rollMax = 30f;
    public float pitchMax = 15f;
    public float suavizado = 6f;

    [Header("Teletransporte")]
    public Vector3 posicionTeletransporte;
    public string nombreEscenaSiguiente;
    public float radioZonaTeletransporte = 2f;

    private Rigidbody rb;
    private PlayerSalud saludScript;
    private float velocidad;
    private float rollActual;
    private float pitchActual;
    private float alturaActual;       // Altura acumulada del avión
    private float alturaObjetivo;     // Altura deseada por Q/E

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        saludScript = GetComponent<PlayerSalud>();

        rb.useGravity = false;
        rb.isKinematic = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        alturaActual = transform.position.y;
        alturaObjetivo = alturaActual;

        // Asignar punto de inicio automático si no está asignado
        if (puntoDeInicio == null)
        {
            GameObject temp = new GameObject("PuntoInicio");
            temp.transform.position = transform.position;
            puntoDeInicio = temp.transform;
        }
    }

    void Update()
    {
        if (!saludScript.EstaVivo()) return;

        ControlVelocidad();
        RotacionVisual();
        ControlAltura();        // Control vertical Q/E suavizado
        RevisarTeletransporte();
    }

    void FixedUpdate()
    {
        if (!saludScript.EstaVivo()) return;

        // ---------------- MOVIMIENTO HORIZONTAL ----------------
        Vector3 movimientoAdelante = transform.forward * velocidad;

        // ---------------- MOVIMIENTO VERTICAL ----------------
        Vector3 movimientoVertical = Vector3.up * alturaActual;

        rb.linearVelocity = movimientoAdelante + movimientoVertical - new Vector3(0f, rb.position.y, 0f);

        // Actualizar posición vertical suavizada
        rb.position = new Vector3(rb.position.x, alturaActual, rb.position.z);
    }

    void ControlVelocidad()
    {
        if (Input.GetKey(KeyCode.W))
            velocidad += aceleracion * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S))
            velocidad -= aceleracion * Time.deltaTime;
        else
            velocidad = Mathf.MoveTowards(velocidad, 0f, friccion * Time.deltaTime);

        velocidad = Mathf.Clamp(velocidad, -velocidadMaxima * 0.3f, velocidadMaxima);
    }

    void RotacionVisual()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Rotación física del avión
        Quaternion nuevaRotacion = Quaternion.Euler(
            0f,
            rb.rotation.eulerAngles.y + h * velocidadGiro * Time.fixedDeltaTime,
            0f
        );
        rb.MoveRotation(nuevaRotacion);

        // Rotación visual del modelo para roll/pitch
        if (modeloNave != null)
        {
            float rollObjetivo = -h * rollMax;
            float pitchObjetivo = -v * pitchMax;

            rollActual = Mathf.Lerp(rollActual, rollObjetivo, Time.deltaTime * suavizado);
            pitchActual = Mathf.Lerp(pitchActual, pitchObjetivo, Time.deltaTime * suavizado);

            modeloNave.localRotation = Quaternion.Euler(pitchActual, 0f, rollActual);
        }
    }

    void ControlAltura()
    {
        // Entrada vertical
        float inputVertical = 0f;
        if (Input.GetKey(KeyCode.Q))
            inputVertical = 1f;
        else if (Input.GetKey(KeyCode.E))
            inputVertical = -1f;

        // Actualizamos altura objetivo
        alturaObjetivo += inputVertical * velocidadVertical * Time.deltaTime;

        // Suavizamos altura actual hacia la altura objetivo
        alturaActual = Mathf.Lerp(alturaActual, alturaObjetivo, Time.deltaTime * suavizadoVertical);
    }

    // ---------------- TELETRANSPORTE ----------------
    void RevisarTeletransporte()
    {
        float distancia = Vector3.Distance(transform.position, posicionTeletransporte);

        if (distancia <= radioZonaTeletransporte)
        {
            if (!string.IsNullOrEmpty(nombreEscenaSiguiente))
            {
                SceneManager.LoadScene(nombreEscenaSiguiente);
            }
            else
            {
                transform.position = posicionTeletransporte;
                alturaActual = posicionTeletransporte.y;
                alturaObjetivo = posicionTeletransporte.y;
                Debug.Log("¡Teletransportado dentro de la misma escena!");
            }
        }
    }

    // ---------------- RESPWAN ----------------
    public void RespawnPlayer()
    {
        if (puntoDeInicio != null)
        {
            transform.position = puntoDeInicio.position;
            rb.linearVelocity = Vector3.zero; // detener movimiento
            alturaActual = puntoDeInicio.position.y;
            alturaObjetivo = puntoDeInicio.position.y;
            velocidad = 0f; // resetear velocidad
        }
    }
}
