using UnityEngine;

public class BossMorado : MonoBehaviour
{
    public int vida = 1000000;
    public GameObject particulasExplosion;

    public AudioClip sonidoMuerte;  

    public static bool bossMoradoMuerto = false;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Bala"))
        {
            vida--;

            Destroy(col.gameObject);

            if (vida <= 0)
            {
                Morir();
            }
        }
    }

    void Morir()
    {
        bossMoradoMuerto = true;

        // sonido de muerte
        if (sonidoMuerte != null)
            AudioSource.PlayClipAtPoint(sonidoMuerte, transform.position);

        // partículas
        if (particulasExplosion != null)
            Instantiate(particulasExplosion, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
