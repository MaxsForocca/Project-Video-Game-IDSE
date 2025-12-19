using UnityEngine;
using System.Collections;

public class MusicaNivel : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip musicaNormal;
    public AudioClip musicaFinal;
    public AudioClip sonidoExplosion;

    public float retrasoMusicaFinal = 1.0f;

    private bool cambioHecho = false;

    void Start()
    {
        audioSource.clip = musicaNormal;
        audioSource.loop = true;
        audioSource.Play();
    }

    void Update()
    {
        if (!cambioHecho && BossDorado.bossDoradoMuerto)
        {
            cambioHecho = true;
            StartCoroutine(SecuenciaCambio());
        }
    }

    IEnumerator SecuenciaCambio()
    {
        audioSource.Stop();

        if (sonidoExplosion != null)
            AudioSource.PlayClipAtPoint(sonidoExplosion, Camera.main.transform.position);

        yield return new WaitForSeconds(retrasoMusicaFinal);

        audioSource.clip = musicaFinal;
        audioSource.loop = true;
        audioSource.Play();
    }
}
