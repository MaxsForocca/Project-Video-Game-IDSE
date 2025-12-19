using UnityEngine;

public class Nave : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    public int vida = 3;

    public GameObject prefabDisparo;
    public Transform puntoDisparo;
    public float tiempoEntreDisparos = 0.25f;

    private float proximoDisparo = 0f;
    private int direccionX = 1;

    void Update()
    {
        Mover();
        Disparar();
    }

    void Mover()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 movimiento = new Vector3(h, v, 0f).normalized;
        transform.position += movimiento * velocidadMovimiento * Time.deltaTime;

        if (h > 0f)
        {
            direccionX = 1;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (h < 0f)
        {
            direccionX = -1;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    void Disparar()
    {
        if (Input.GetMouseButton(0) && Time.time >= proximoDisparo)
        {
            GameObject disparo = Instantiate(
                prefabDisparo,
                puntoDisparo.position,
                Quaternion.identity
            );

            Bala bala = disparo.GetComponent<Bala>();
            if (bala != null)
            {
                bala.SetDireccion(direccionX);
            }

            proximoDisparo = Time.time + tiempoEntreDisparos;
        }
    }

    public void RecibirDanio(int danio)
    {
        vida -= danio;

        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}
