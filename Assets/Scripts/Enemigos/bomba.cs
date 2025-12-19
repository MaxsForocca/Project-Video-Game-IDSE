using UnityEngine;

public class Bomba : MonoBehaviour
{
    public float velocidad = 3f;
    public float amplitud = 1f;
    public float frecuencia = 2f;
    public int danio = 1;

    public GameObject efectoExplosion;
    public float duracionExplosion = 1f;
    public float radioExplosion = 3f;

    public float tiempoMaximoVida = 7f;

    private Vector3 posicionInicial;
    private bool explotando = false;
    private float tiempoVida = 0f;

    private Rigidbody cuerpoRigido;
    private float direccionX = -1f;

    public void SetDireccion(bool haciaDerecha)
    {
        direccionX = haciaDerecha ? 1f : -1f;
    }

    void Start()
    {
        posicionInicial = transform.position;

        cuerpoRigido = GetComponent<Rigidbody>();
        if (cuerpoRigido != null)
        {
            cuerpoRigido.useGravity = false;
            cuerpoRigido.isKinematic = true;
        }
    }

    void Update()
    {
        if (explotando) return;

        MoverBomba();
        VerificarTiempoVida();
    }

    void MoverBomba()
    {
        float nuevoY = posicionInicial.y + Mathf.Sin(tiempoVida * frecuencia) * amplitud;

        transform.position = new Vector3(
            transform.position.x + direccionX * velocidad * Time.deltaTime,
            nuevoY,
            transform.position.z
        );
    }

    void VerificarTiempoVida()
    {
        tiempoVida += Time.deltaTime;

        if (tiempoVida >= tiempoMaximoVida)
        {
            Explotar();
        }
    }

    void OnTriggerEnter(Collider otro)
    {
        if (explotando) return;

        if (otro.CompareTag("Nave") || otro.CompareTag("Bala"))
        {
            Explotar();
        }
    }

    void Explotar()
    {
        explotando = true;

        if (efectoExplosion != null)
        {
            GameObject explosion = Instantiate(
                efectoExplosion,
                transform.position,
                Quaternion.identity
            );

            Destroy(explosion, duracionExplosion);
        }

        AplicarDanioAlrededor();

        Destroy(gameObject);
    }

    void AplicarDanioAlrededor()
    {
        Collider[] objetosCercanos =
            Physics.OverlapSphere(transform.position, radioExplosion);

        foreach (Collider col in objetosCercanos)
        {
            if (col.CompareTag("Nave"))
            {
                Nave nave = col.GetComponent<Nave>();
                if (nave != null)
                {
                    nave.RecibirDanio(danio);
                }
            }
        }
    }
}
