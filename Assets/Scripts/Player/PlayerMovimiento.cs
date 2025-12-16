using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovimientoAvion : MonoBehaviour
{
    [Header("Sonido")]
    public AudioSource motor;

    [Header("Velocidad y Movimiento")]
    public float velocidadMaxima = 70f;
    public float aceleracion = 20f;
    public float friccion = 5f;

    public float velocidadGiro = 90f;
    public float rollSpeed = 50f;
    public float pitchSpeed = 30f; // Para subir/bajar con inclinación

    public float velocidadAscenso = 20f;
    public float alturaMax = 50f;
    public float alturaMin = 0f;

    private Rigidbody rb;
    private float velocidad = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.linearDamping = 0.1f;
        rb.angularDamping = 0.05f;
    }

    void Update()
    {
        ProcesarTeclado();
        Rotar();
        ActualizarSonidoMotor();
    }

    void FixedUpdate()
    {
        // Movimiento adelante/atrás
        Vector3 mover = transform.forward * velocidad * Time.fixedDeltaTime;

        // Ascenso / descenso usando W y S
        float altInput = 0f;
        if (Input.GetKey(KeyCode.W)) altInput = 1f;  // Subir
        if (Input.GetKey(KeyCode.S)) altInput = -1f; // Bajar

        Vector3 altura = Vector3.up * altInput * velocidadAscenso * Time.fixedDeltaTime;

        // Descenso natural si no se presiona nada
        if (altInput == 0f && transform.position.y > alturaMin)
        {
            altura = Vector3.down * velocidadAscenso * 0.5f * Time.fixedDeltaTime;
        }

        rb.MovePosition(rb.position + mover + altura);
    }

    private void ProcesarTeclado()
    {
        bool teclaPresionada = false;

        // Aceleración hacia adelante/atrás
        if (Input.GetKey(KeyCode.W))
        {
            teclaPresionada = true;
            if (velocidad < velocidadMaxima)
                velocidad += aceleracion * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            teclaPresionada = true;
            if (velocidad > -velocidadMaxima / 5f)
                velocidad -= aceleracion * Time.deltaTime;
        }

        // Fricción natural
        if (!teclaPresionada)
        {
            if (velocidad > 0)
                velocidad -= friccion * Time.deltaTime;
            else if (velocidad < 0)
                velocidad += friccion * Time.deltaTime;
        }

        // Limitar velocidad
        velocidad = Mathf.Clamp(velocidad, -velocidadMaxima / 5f, velocidadMaxima);
    }

    private void Rotar()
    {
        // Giro horizontal (yaw)
        float giroY = 0f;
        if (Input.GetKey(KeyCode.A)) giroY = -velocidadGiro * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) giroY = velocidadGiro * Time.deltaTime;

        // Roll (inclinación lateral)
        float roll = 0f;
        if (Input.GetKey(KeyCode.Q)) roll = rollSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.E)) roll = -rollSpeed * Time.deltaTime;

        // Pitch (inclinación al subir/bajar)
        float pitch = 0f;
        if (Input.GetKey(KeyCode.W)) pitch = -pitchSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) pitch = pitchSpeed * Time.deltaTime;

        // Aplicar rotación
        rb.MoveRotation(rb.rotation * Quaternion.Euler(pitch, giroY, roll));
    }

    private void ActualizarSonidoMotor()
    {
        if (motor == null) return;
        motor.pitch = Mathf.Clamp(velocidad / 10f, 0.5f, 1.5f);
        if (!motor.isPlaying)
            motor.Play();
    }

    public float GetVelocidad()
    {
        return velocidad;
    }
}
