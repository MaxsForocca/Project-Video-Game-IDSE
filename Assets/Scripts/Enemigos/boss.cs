using UnityEngine;
using System.Collections;

public class BossIA : MonoBehaviour
{
    public Transform jugador;
    public GameObject bombaPrefab;

    public float tiempoDescanso = 5f;
    public float probabilidadSalto = 0.5f;

    public float alturaMaximaSalto = 4f;
    public float duracionSalto = 1.5f;
    public int danioSalto = 2;
    public float radioDanioSalto = 5f;

    public float fuerzaLanzamientoBomba = 18f;

    public float esquinaIzquierdaX = -15f;
    public float esquinaDerechaX = 15f;

    private Rigidbody rb;
    private bool atacando = false;
    private bool enEsquinaDerecha = false;
    private bool enSuelo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ConfigurarFisica();

        if (jugador == null)
        {
            GameObject jugadorObj = GameObject.FindGameObjectWithTag("Nave");
            if (jugadorObj != null) jugador = jugadorObj.transform;
        }

        enEsquinaDerecha = false;
        PosicionarEnEsquina();

        StartCoroutine(CicloAtaqueDescanso());
    }

    void ConfigurarFisica()
    {
        rb.useGravity = true;
        rb.mass = 50f;
        rb.linearDamping = 1f;
        rb.angularDamping = 2f;
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }

    void PosicionarEnEsquina()
    {
        float posX = enEsquinaDerecha ? esquinaDerechaX : esquinaIzquierdaX;
        transform.position = new Vector3(posX, 0, 0);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    IEnumerator CicloAtaqueDescanso()
    {
        while (true)
        {
            if (!atacando && enSuelo && jugador != null)
            {
                if (Random.value < probabilidadSalto)
                {
                    yield return StartCoroutine(SaltoParabolico());
                }
                else
                {
                    yield return StartCoroutine(LanzarBombasDirectas());
                }

                yield return new WaitForSeconds(tiempoDescanso);
            }

            yield return null;
        }
    }

    IEnumerator SaltoParabolico()
    {
        atacando = true;
        enSuelo = false;

        float posicionInicialX = transform.position.x;
        float posicionObjetivoX = enEsquinaDerecha ? esquinaIzquierdaX : esquinaDerechaX;
        float distanciaX = Mathf.Abs(posicionObjetivoX - posicionInicialX);

        enEsquinaDerecha = !enEsquinaDerecha;

        float velocidadHorizontal = distanciaX / duracionSalto;
        float velocidadVertical = Mathf.Sqrt(2f * Mathf.Abs(Physics.gravity.y) * alturaMaximaSalto);

        float direccionX = (posicionObjetivoX > posicionInicialX) ? 1f : -1f;
        rb.linearVelocity = new Vector3(direccionX * velocidadHorizontal, velocidadVertical, 0);

        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracionSalto)
        {
            tiempoTranscurrido += Time.deltaTime;
            float progreso = tiempoTranscurrido / duracionSalto;

            float posX = Mathf.Lerp(posicionInicialX, posicionObjetivoX, progreso);
            float altura = CalcularAlturaParabolica(progreso, alturaMaximaSalto);

            transform.position = new Vector3(posX, altura, 0);

            yield return null;
        }

        transform.position = new Vector3(posicionObjetivoX, 0, 0);
        rb.linearVelocity = Vector3.zero;

        AplicarDanioSalto();

        atacando = false;
    }

    float CalcularAlturaParabolica(float progreso, float alturaMax)
    {
        float x = progreso * 2f - 1f;
        float altura = alturaMax * (1f - x * x);
        return Mathf.Max(0, altura);
    }

    void AplicarDanioSalto()
    {
        Collider[] objetosCercanos = Physics.OverlapSphere(transform.position, radioDanioSalto);

        foreach (Collider col in objetosCercanos)
        {
            if (col.CompareTag("Nave"))
            {
                Nave nave = col.GetComponent<Nave>();
                if (nave != null)
                {
                    nave.RecibirDanio(danioSalto);
                }
            }
        }
    }

    IEnumerator LanzarBombasDirectas()
    {
        atacando = true;

        int cantidadBombas = Random.Range(2, 4);

        for (int i = 0; i < cantidadBombas; i++)
        {
            if (jugador != null && enSuelo)
            {
                LanzarBombaDirecta();
            }

            yield return new WaitForSeconds(0.4f);
        }

        atacando = false;
    }

    void LanzarBombaDirecta()
    {
        if (bombaPrefab == null || jugador == null) return;

        Vector3 posicionLanzamiento = transform.position;

        GameObject bomba = Instantiate(bombaPrefab, posicionLanzamiento, Quaternion.identity);

        Rigidbody rbBomba = bomba.GetComponent<Rigidbody>();
        if (rbBomba != null)
        {
            rbBomba.useGravity = false;
            rbBomba.linearDamping = 0f;
            rbBomba.angularDamping = 0.5f;

            Vector3 direccion = (jugador.position - posicionLanzamiento).normalized;

            rbBomba.AddForce(direccion * fuerzaLanzamientoBomba, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = true;
            Vector3 pos = transform.position;
            pos.y = 0;
            transform.position = pos;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = false;
        }
    }

    void Update()
    {
        if (enSuelo && Mathf.Abs(transform.position.y) > 0.1f)
        {
            Vector3 pos = transform.position;
            pos.y = 0;
            transform.position = pos;
        }
    }
}