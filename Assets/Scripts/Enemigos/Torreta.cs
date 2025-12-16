using UnityEngine;

public class Torreta : Enemigo
{
    public GameObject balaPrefab;
    public Transform puntoDisparo;
    public float fuerzaBala = 400f;

    public int disparosPorRafaga = 3;
    public float tiempoEntreRafagas = 4f;

    void Start()
    {
        InvokeRepeating(nameof(Rafaga), 2f, tiempoEntreRafagas);
    }

    void Rafaga()
    {
        StartCoroutine(DispararRafaga());
    }

    System.Collections.IEnumerator DispararRafaga()
    {
        for (int i = 0; i < disparosPorRafaga; i++)
        {
            GameObject bala = Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
            bala.GetComponent<Rigidbody>().AddForce(puntoDisparo.forward * fuerzaBala);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
