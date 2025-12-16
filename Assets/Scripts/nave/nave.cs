using UnityEngine;

public class Nave : MonoBehaviour
{
    public float velocidadMovimiento = 10f;
    public float fuerzaDisparo = 800f;
    public float amortiguacionLineal = 3f;
    public float amortiguacionAngular = 5f;
    public int vida = 3;
    public GameObject balaPrefab;
    public Transform puntoDisparo;
    public GameObject efectoExplosion;
    public float duracionExplosion = 1f;

    private Rigidbody cuerpoRigido;
    private bool estaMuerta = false;

    void Start()
    {
        cuerpoRigido = GetComponent<Rigidbody>();
        ConfigurarFisica();
    }

    void FixedUpdate()
    {
        if (!estaMuerta)
        {
            MoverNave();
        }
    }

    void Update()
    {
        if (!estaMuerta)
        {
            Disparar();
            VerificarMuerte();
        }
    }

    void ConfigurarFisica()
    {
        cuerpoRigido.useGravity = false;
        cuerpoRigido.linearDamping = amortiguacionLineal;
        cuerpoRigido.angularDamping = amortiguacionAngular;

        cuerpoRigido.constraints = RigidbodyConstraints.FreezeRotationX |
                                   RigidbodyConstraints.FreezeRotationY |
                                   RigidbodyConstraints.FreezeRotationZ |
                                   RigidbodyConstraints.FreezePositionZ;
    }

    void MoverNave()
    {
        float moverHorizontal = Input.GetAxis("Horizontal");
        float moverVertical = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(moverHorizontal, moverVertical, 0);
        movimiento = movimiento.normalized * velocidadMovimiento;

        cuerpoRigido.AddForce(movimiento, ForceMode.Force);
    }

    void Disparar()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 direccionDisparo = Vector3.right;

            GameObject bala = Instantiate(balaPrefab, puntoDisparo.position, Quaternion.identity);

            Rigidbody cuerpoBala = bala.GetComponent<Rigidbody>();
            if (cuerpoBala != null)
            {
                cuerpoBala.AddForce(direccionDisparo * fuerzaDisparo, ForceMode.Impulse);
            }

            bala.transform.rotation = transform.rotation;
        }
    }

    public void RecibirDanio(int cantidadDanio)
    {
        if (!estaMuerta)
        {
            vida -= cantidadDanio;
        }
    }

    void VerificarMuerte()
    {
        if (!estaMuerta && vida <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        estaMuerta = true;

        CrearExplosion();

        DetenerMovimiento();

        DesactivarNave();

        Destroy(gameObject, duracionExplosion);
    }

    void CrearExplosion()
    {
        if (efectoExplosion != null)
        {
            GameObject explosion = Instantiate(efectoExplosion, transform.position, Quaternion.identity);
            Destroy(explosion, duracionExplosion);
        }
    }

    void DetenerMovimiento()
    {
        Vector3 fuerzaContraria = -cuerpoRigido.linearVelocity * cuerpoRigido.mass;
        cuerpoRigido.AddForce(fuerzaContraria, ForceMode.Impulse);

        Vector3 torqueContrario = -cuerpoRigido.angularVelocity * cuerpoRigido.mass;
        cuerpoRigido.AddTorque(torqueContrario, ForceMode.Impulse);
    }

    void DesactivarNave()
    {
        Renderer rendererNave = GetComponent<Renderer>();
        if (rendererNave != null)
        {
            rendererNave.enabled = false;
        }

        Collider colliderNave = GetComponent<Collider>();
        if (colliderNave != null)
        {
            colliderNave.enabled = false;
        }

        cuerpoRigido.isKinematic = true;

        foreach (Transform hijo in transform)
        {
            Renderer rendererHijo = hijo.GetComponent<Renderer>();
            if (rendererHijo != null)
            {
                rendererHijo.enabled = false;
            }
        }
    }

    void OnTriggerEnter(Collider otro)
    {
        if (!estaMuerta && otro.CompareTag("Bomba"))
        {
            Bomba bomba = otro.GetComponent<Bomba>();
            if (bomba != null)
            {
                RecibirDanio(bomba.danio);
            }
        }
    }
}