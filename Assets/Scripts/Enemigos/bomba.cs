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
    public float distanciaActivacion = 20f;
    public float tiempoMaximoVida = 7f;

    private Vector3 posicionInicial;
    private bool explotando = false;
    private bool activa = false;
    private Rigidbody cuerpoRigido;
    private float tiempoActiva = 0f;
    private GameObject nave;
    private Renderer rendererBomba;
    private Collider colliderBomba;

    void Start()
    {
        posicionInicial = transform.position;
        cuerpoRigido = GetComponent<Rigidbody>();
        rendererBomba = GetComponent<Renderer>();
        colliderBomba = GetComponent<Collider>();

        nave = GameObject.FindGameObjectWithTag("Nave");

        if (cuerpoRigido != null)
        {
            cuerpoRigido.useGravity = false;
            cuerpoRigido.isKinematic = true;
        }

        if (colliderBomba != null)
        {
            colliderBomba.enabled = false;
        }

        if (rendererBomba != null)
        {
            rendererBomba.enabled = false;
        }
    }

    void Update()
    {
        if (explotando) return;

        if (!activa)
        {
            VerificarActivacion();
        }
        else
        {
            MoverBomba();
            VerificarTiempoVida();
        }
    }

    void VerificarActivacion()
    {
        if (nave != null)
        {
            float distancia = Vector3.Distance(transform.position, nave.transform.position);

            if (distancia <= distanciaActivacion)
            {
                ActivarBomba();
            }
        }
    }

    void ActivarBomba()
    {
        activa = true;
        posicionInicial = transform.position;

        if (cuerpoRigido != null)
        {
            cuerpoRigido.isKinematic = false;
        }

        if (colliderBomba != null)
        {
            colliderBomba.enabled = true;
        }

        if (rendererBomba != null)
        {
            rendererBomba.enabled = true;
        }
    }

    void MoverBomba()
    {
        float nuevoY = posicionInicial.y + Mathf.Sin(Time.time * frecuencia) * amplitud;

        transform.position = new Vector3(
            transform.position.x - velocidad * Time.deltaTime,
            nuevoY,
            transform.position.z
        );
    }

    void VerificarTiempoVida()
    {
        tiempoActiva += Time.deltaTime;

        if (tiempoActiva >= tiempoMaximoVida)
        {
            Explotar();
        }
    }

    void OnTriggerEnter(Collider otro)
    {
        if (explotando || !activa) return;

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
            GameObject explosion = Instantiate(efectoExplosion, transform.position, Quaternion.identity);
            Destroy(explosion, duracionExplosion);
        }

        AplicarDanioAlrededor();

        DesactivarBombaVisual();

        Destroy(gameObject, duracionExplosion);
    }

    void AplicarDanioAlrededor()
    {
        Collider[] objetosCercanos = Physics.OverlapSphere(transform.position, radioExplosion);

        foreach (Collider colisionador in objetosCercanos)
        {
            if (colisionador.CompareTag("Nave"))
            {
                Nave naveComponente = colisionador.GetComponent<Nave>();
                if (naveComponente != null)
                {
                    naveComponente.RecibirDanio(danio);
                }
            }
        }
    }

    void DesactivarBombaVisual()
    {
        if (rendererBomba != null)
        {
            rendererBomba.enabled = false;
        }

        if (colliderBomba != null)
        {
            colliderBomba.enabled = false;
        }

        if (cuerpoRigido != null)
        {
            cuerpoRigido.isKinematic = true;
        }
    }
}