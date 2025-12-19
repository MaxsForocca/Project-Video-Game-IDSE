using UnityEngine;

public class BalaEnemiga : MonoBehaviour
{
    public float velocidad = 15f;
    public int daño = 1;

    void Update()
    {
        transform.Translate(Vector3.left * velocidad * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Player"))
        {
            VidaNave vidaDelJugador = otro.GetComponent<VidaNave>();

            if (vidaDelJugador != null)
            {
                vidaDelJugador.RecibirDaño(daño);
            }

            Destroy(gameObject);
        }
    }

    void Start()
    {
        Destroy(gameObject, 5f);
    }
}