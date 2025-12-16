using UnityEngine;
using System.Collections; // Necesario para las Corrutinas (la cinem�tica)

public class ControladorDron : MonoBehaviour
{
    [Header("Configuraci�n de Vuelo")]
    public float velocidad = 10f;
    private Rigidbody rb;
    private Vector2 movimientoInput;

    [Header("Configuraci�n de Despegue")]
    public bool estaDespegando = true; // Empieza en verdadero
    public float velocidadDespegue = 2f;
    public float tiempoDeDespegue = 3f; // Cu�ntos segundos tarda en salir

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Al empezar el juego, iniciamos la secuencia autom�tica
        StartCoroutine(SecuenciaDespegue());
    }

    void Update()
    {
        // Si est� despegando, NO leemos los controles del jugador
        if (estaDespegando) return;

        // Leemos las teclas WASD o Flechas (valores entre -1 y 1)
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        // Guardamos la direcci�n en un vector 2D
        movimientoInput = new Vector2(inputX, inputY).normalized;
    }

    void FixedUpdate()
    {
        // Si est� despegando, la f�sica la controla la corrutina
        if (estaDespegando) return;

        // Aplicamos el movimiento normal usando f�sica
        // Mantenemos la velocidad Z actual (que deber�a ser 0 por el Rigidbody freeze)
        Vector3 velocidadFinal = new Vector3(movimientoInput.x * velocidad, movimientoInput.y * velocidad, rb.linearVelocity.z);
        rb.linearVelocity = velocidadFinal;
    }

    // ---- CINEM�TICA DE INICIO ----
    // Esto es una "Corrutina", permite hacer cosas que esperan en el tiempo
    IEnumerator SecuenciaDespegue()
    {
        Debug.Log("Iniciando sistemas... Despegando.");

        // Peque�a pausa dram�tica antes de moverse (opcional)
        yield return new WaitForSeconds(1f);

        float tiempoPasado = 0f;

        // Mientras no haya pasado el tiempo estipulado...
        while (tiempoPasado < tiempoDeDespegue)
        {
            // Movemos el dron solo hacia ARRIBA suavemente
            rb.linearVelocity = Vector3.up * velocidadDespegue;

            // Sumamos el tiempo que ha pasado en este frame
            tiempoPasado += Time.deltaTime;

            // Esperamos al siguiente frame
            yield return null;
        }

        // Al terminar el tiempo, frenamos el impulso hacia arriba
        rb.linearVelocity = Vector3.zero;

        Debug.Log("Despegue completado. Control manual activado.");
        // �Damos el control al jugador!
        estaDespegando = false;
    }
}