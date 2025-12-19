using UnityEngine;

public class PlayerDisparoRafaga : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject balaPrefab;
    public Transform puntoDisparo;
    public RectTransform mira;
    public Camera camara;

    [Header("Disparo")]
    public float velocidadBala = 150f;
    public float cooldown = 0.08f;
    public float distanciaMaxima = 500f;

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

        Ray ray = camara.ScreenPointToRay(mira.position);
        Vector3 objetivo = Physics.Raycast(ray, out RaycastHit hit, distanciaMaxima)
            ? hit.point
            : ray.origin + ray.direction * distanciaMaxima;

        Vector3 direccion = (objetivo - puntoDisparo.position).normalized;

        GameObject bala = Instantiate(balaPrefab, puntoDisparo.position, Quaternion.LookRotation(direccion));
        bala.transform.localScale = Vector3.one * 0.2f;

        Rigidbody rb = bala.GetComponent<Rigidbody>() ?? bala.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.linearVelocity = direccion * velocidadBala;

        SphereCollider col = bala.GetComponent<SphereCollider>() ?? bala.AddComponent<SphereCollider>();
        col.isTrigger = true;
        col.radius = 0.1f;

        BalaPlayerLogica logica = bala.GetComponent<BalaPlayerLogica>() ?? bala.AddComponent<BalaPlayerLogica>();
        logica.daño = 15;
    }
}
