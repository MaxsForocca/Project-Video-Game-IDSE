using UnityEngine;
using System.Collections;

public class ControladorDron : MonoBehaviour
{
    [Header("Configuracin de Vuelo")]
    public float velocidad = 10f;
    private Rigidbody rb;
    private Vector2 movimientoInput;

    [Header("Configuracin de Despegue")]
    public bool estaDespegando = true;
    public float velocidadDespegue = 2f;
    public float tiempoDeDespegue = 3f;

    [Header("Límites de la Cámara")]
    public float techoY = 4.5f;      // Límite Arriba
    public float sueloY = -4.5f;     // Límite Abajo
    // HE BORRADO LOS LÍMITES DE IZQUIERDA Y DERECHA AQUÍ

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(SecuenciaDespegue());
    }

    void Update()
    {
        if (estaDespegando) return;

        // Leemos inputs
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        // --- LÓGICA DE LÍMITES (SOLO VERTICAL) ---

        // Tomamos la posición actual
        float yLimitada = transform.position.y;

        // Aplicamos el "cepo" SOLO en Y (Arriba/Abajo)
        yLimitada = Mathf.Clamp(yLimitada, sueloY, techoY);

        // Aplicamos la posición:
        // En X: Dejamos la que tiene (transform.position.x) para que se mueva libre.
        // En Y: Ponemos la limitada.
        transform.position = new Vector3(transform.position.x, yLimitada, transform.position.z);

        // Guardamos la direccin para las físicas
        movimientoInput = new Vector2(inputX, inputY).normalized;
    }

    void FixedUpdate()
    {
        if (estaDespegando) return;

        // Movimiento físico
        Vector3 velocidadFinal = new Vector3(movimientoInput.x * velocidad, movimientoInput.y * velocidad, rb.linearVelocity.z);
        rb.linearVelocity = velocidadFinal;
    }

    // ---- CINEMTICA DE INICIO ----
    IEnumerator SecuenciaDespegue()
    {
        Debug.Log("Iniciando sistemas... Despegando.");

        yield return new WaitForSeconds(1f);

        float tiempoPasado = 0f;

        while (tiempoPasado < tiempoDeDespegue)
        {
            rb.linearVelocity = Vector3.up * velocidadDespegue;
            tiempoPasado += Time.deltaTime;
            yield return null;
        }

        rb.linearVelocity = Vector3.zero;

        Debug.Log("Despegue completado. Control manual activado.");
        estaDespegando = false;
    }
}