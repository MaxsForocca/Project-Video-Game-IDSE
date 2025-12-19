using UnityEngine;

public class Torreta : MonoBehaviour
{
    [Header("Referencias")]
    public Transform cabeza;           // la parte que rota
    public Transform[] puntosDisparo;  // dos puntos de disparo
    public GameObject prefabBala;      // prefab de bala
    public Transform jugador;          // referencia al jugador

    [Header("Parámetros")]
    public float velocidadRotacion = 5f;
    public float rangoDisparo = 30f;
    public float fireRate = 1.5f;

    private float tiempoDisparo;

    void Update()
    {
        if (jugador == null) return;

        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia <= rangoDisparo)
        {
            RotarHaciaJugador();
            Disparar();
        }
    }

    void RotarHaciaJugador()
    {
        Vector3 direccion = jugador.position - cabeza.position;
        Quaternion rotacionDeseada = Quaternion.LookRotation(direccion);
        cabeza.rotation = Quaternion.Slerp(cabeza.rotation, rotacionDeseada, velocidadRotacion * Time.deltaTime);
    }

    void Disparar()
    {
        if (Time.time >= tiempoDisparo)
        {
            foreach (Transform punto in puntosDisparo)
            {
                GameObject bala = Instantiate(prefabBala, punto.position, punto.rotation);
                bala.GetComponent<Bala>().Inicializar(jugador);
            }

            tiempoDisparo = Time.time + fireRate;
        }
    }
}
