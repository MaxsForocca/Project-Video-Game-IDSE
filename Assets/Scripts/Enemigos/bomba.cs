using UnityEngine;

public class Bomba : MonoBehaviour
{
    public float velocidad = 3f;
    public float amplitud = 1f;
    public float frecuencia = 2f;
    public double danio = 0.05;

    public GameObject efectoExplosion;
    public float duracionExplosion = 1f;
    public float tiempoMaximoVida = 7f;

    private Vector3 posicionInicial;
    private float tiempoVida = 0f;
    private bool explotando = false;
    private bool yaHizoDanio = false;

    private Rigidbody rb;
    private Collider col;
    private float direccionX = -1f;

    public void SetDireccion(bool haciaDerecha)
    {
        direccionX = haciaDerecha ? 1f : -1f;
    }

    void Start()
    {
        posicionInicial = transform.position;

        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = false;
        rb.constraints =
            RigidbodyConstraints.FreezeRotation |
            RigidbodyConstraints.FreezePositionZ;

        col = GetComponent<Collider>();
    }

    void Update()
    {
        if (explotando) return;

        tiempoVida += Time.deltaTime;
        if (tiempoVida >= tiempoMaximoVida)
            Explotar();

        float nuevoY = posicionInicial.y + Mathf.Sin(tiempoVida * frecuencia) * amplitud;

        rb.MovePosition(new Vector3(
            rb.position.x + direccionX * velocidad * Time.deltaTime,
            nuevoY,
            rb.position.z
        ));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (yaHizoDanio || explotando) return;

        if (collision.gameObject.CompareTag("Nave"))
        {
            yaHizoDanio = true;
            explotando = true;

            if (col != null) col.enabled = false;

            Nave nave = collision.gameObject.GetComponent<Nave>();
            if (nave != null)
                nave.RecibirDanio(danio);

            Explotar();
        }
        else if (collision.gameObject.CompareTag("Bala"))
        {
            Explotar();
        }
    }

    void Explotar()
    {
        explotando = true;

        if (col != null) col.enabled = false;

        if (efectoExplosion != null)
        {
            GameObject fx = Instantiate(
                efectoExplosion,
                transform.position,
                Quaternion.identity
            );
            Destroy(fx, duracionExplosion);
        }

        Destroy(gameObject);
    }
}
