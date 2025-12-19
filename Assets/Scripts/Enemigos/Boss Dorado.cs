using UnityEngine;

public class BossDorado : MonoBehaviour
{
    public int vida = 100000;
    public GameObject particulasExplosion;

    public AudioClip sonidoMuerte;   // ← NUEVO

    public static bool bossDoradoMuerto = false;

    void OnCollisionEnter(Collision col)
    {
        if (!BossMorado.bossMoradoMuerto)
            return;

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
        bossDoradoMuerto = true;

        // sonido de muerte
        if (sonidoMuerte != null)
            AudioSource.PlayClipAtPoint(sonidoMuerte, transform.position);

        // partículas
        if (particulasExplosion != null)
            Instantiate(particulasExplosion, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}

