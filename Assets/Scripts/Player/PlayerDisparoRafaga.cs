using UnityEngine;

public class PlayerDisparoRafaga : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject balaPrefab;          // Prefab de bala
    public Transform puntoDisparo;         // Empty delante de la nave
    public RectTransform mira;             // UI crosshair
    public Camera camara;                  // Main Camera

    [Header("Disparo")]
    public float velocidadBala = 150f;     // Velocidad de la bala
    public float cooldown = 0.08f;         // Tiempo entre disparos
    public float distanciaMaxima = 500f;   // Alcance máximo

    private float siguienteDisparo;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= siguienteDisparo)
        {
            Disparar();
            siguienteDisparo = Time.time + cooldown;
        }
    }

    void Disparar()
    {
        if (!balaPrefab || !puntoDisparo || !mira || !camara) return;

        // Ray desde la cámara hacia la posición de la mira
        Ray ray = camara.ScreenPointToRay(mira.position);

        Vector3 puntoObjetivo;
        if (Physics.Raycast(ray, out RaycastHit hit, distanciaMaxima))
        {
            puntoObjetivo = hit.point;
        }
        else
        {
            puntoObjetivo = ray.origin + ray.direction * distanciaMaxima;
        }

        // Dirección
        Vector3 direccion = (puntoObjetivo - puntoDisparo.position).normalized;

        // Instanciar bala
        GameObject bala = Instantiate(balaPrefab, puntoDisparo.position, Quaternion.LookRotation(direccion));
        bala.transform.localScale = Vector3.one * 0.2f;

        // Rigidbody
        Rigidbody rb = bala.GetComponent<Rigidbody>();
        if (rb == null) rb = bala.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        // Velocidad
        rb.linearVelocity = direccion * velocidadBala;

        // Collider
        if (bala.GetComponent<Collider>() == null)
        {
            SphereCollider col = bala.AddComponent<SphereCollider>();
            col.isTrigger = false;
            col.radius = 0.1f;
        }

        // Debug
        Debug.DrawRay(puntoDisparo.position, direccion * 5f, Color.red, 0.5f);
    }
}
