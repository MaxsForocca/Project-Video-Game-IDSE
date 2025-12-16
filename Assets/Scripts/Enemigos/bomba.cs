using UnityEngine;

public class Bomba : MonoBehaviour
{
    public float velocidad = 3f;
    public float amplitud = 1f;
    public float frecuencia = 2f;
    public int danio = 1;
    public GameObject efectoExplosion;
    public float duracionExplosion = 1f;

    private Vector3 posicionInicial;
    private bool explotando = false;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        if (!explotando)
        {
            float nuevoY = posicionInicial.y + Mathf.Sin(Time.time * frecuencia) * amplitud;

            transform.position = new Vector3(
                transform.position.x - velocidad * Time.deltaTime,
                nuevoY,
                transform.position.z
            );
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

    void OnBecameInvisible()
    {
        if (!explotando)
        {
            Destroy(gameObject);
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

        Renderer rendererBomba = GetComponent<Renderer>();
        if (rendererBomba != null)
        {
            rendererBomba.enabled = false;
        }

        Collider colliderBomba = GetComponent<Collider>();
        if (colliderBomba != null)
        {
            colliderBomba.enabled = false;
        }

        Destroy(gameObject, duracionExplosion);
    }

    void AplicarDanioAlrededor()
    {
        Collider[] objetosCercanos = Physics.OverlapSphere(transform.position, 3f);

        foreach (Collider colisionador in objetosCercanos)
        {
            if (colisionador.CompareTag("Nave"))
            {
                Nave nave = colisionador.GetComponent<Nave>();
                if (nave != null)
                {
                    nave.RecibirDanio(danio);
                }
            }
        }
    }
}