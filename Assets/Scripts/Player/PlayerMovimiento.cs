using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerSalud))]
public class PlayerMovimiento : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidadMaxima = 30f;   // Ajustado más lento
    public float aceleracion = 3f;        // Ajustado más lento
    public float friccion = 5f;
    public float velocidadGiro = 50f;

    [Header("Inclinación visual")]
    public Transform modeloNave;
    public float rollMax = 30f;
    public float pitchMax = 15f;
    public float suavizado = 6f;

    [Header("Altura fija")]
    public bool usarAlturaFija = true;

    [Header("Teletransporte")]
    public Vector3 posicionTeletransporte; // Coordenadas a donde se teletransporta
    public string nombreEscenaSiguiente;   // Opcional, si quieres cambiar de escena
    public float radioZonaTeletransporte = 2f; // Radio de activación

    private Rigidbody rb;
    private PlayerSalud saludScript;
    private float velocidad;
    private float rollActual;
    private float pitchActual;
    private float alturaInicial;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        saludScript = GetComponent<PlayerSalud>();

        rb.useGravity = false;
        rb.isKinematic = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        alturaInicial = transform.position.y;
    }

    void Update()
    {
        if (!saludScript.EstaVivo()) return;

        ControlVelocidad();
        RotacionVisual();
        RevisarTeletransporte();
    }

    void FixedUpdate()
    {
        if (!saludScript.EstaVivo()) return;

        Vector3 velocidadFinal = transform.forward * velocidad;
        rb.linearVelocity = new Vector3(velocidadFinal.x, 0f, velocidadFinal.z);

        if (usarAlturaFija)
        {
            rb.position = new Vector3(rb.position.x, alturaInicial, rb.position.z);
        }
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

        Quaternion nuevaRotacion = Quaternion.Euler(
            0f,
            rb.rotation.eulerAngles.y + h * velocidadGiro * Time.fixedDeltaTime,
            0f
        );
        rb.MoveRotation(nuevaRotacion);

        if (modeloNave != null)
        {
            float rollObjetivo = -h * rollMax;
            float pitchObjetivo = -v * pitchMax;

            rollActual = Mathf.Lerp(rollActual, rollObjetivo, Time.deltaTime * suavizado);
            pitchActual = Mathf.Lerp(pitchActual, pitchObjetivo, Time.deltaTime * suavizado);

            modeloNave.localRotation = Quaternion.Euler(pitchActual, 0f, rollActual);
        }
    }

    // ---------------- TELETRANSPORTE ----------------
    void RevisarTeletransporte()
    {
        float distancia = Vector3.Distance(transform.position, posicionTeletransporte);

        if (distancia <= radioZonaTeletransporte)
        {
            if (!string.IsNullOrEmpty(nombreEscenaSiguiente))
            {
                // Teletransportar a otra escena
                SceneManager.LoadScene(nombreEscenaSiguiente);
            }
            else
            {
                // Teletransportar a posición dentro de la misma escena
                transform.position = posicionTeletransporte;
                Debug.Log("¡Teletransportado dentro de la misma escena!");
            }
        }
    }
}
