using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinalNivel : MonoBehaviour
{
    public Nave nave;

    public float descenso = 2f;
    public float velocidadDescenso = 1.5f;
    public float velocidadDerecha = 1f;

    public TextMeshProUGUI textoFin;
    public TextMeshProUGUI textoGracias;

    public CanvasGroup fadeNegro;

    public float duracionTexto = 5f;
    public float duracionFade = 2f;

    bool iniciado = false;

    void Awake()
    {
        if (nave == null)
            nave = FindFirstObjectByType<Nave>();

        if (textoFin != null)
            textoFin.gameObject.SetActive(false);

        if (textoGracias != null)
            textoGracias.gameObject.SetActive(false);

        if (fadeNegro != null)
            fadeNegro.alpha = 0f;
    }

    void Update()
    {
        if (!iniciado && BossDorado.bossDoradoMuerto)
        {
            iniciado = true;
            StartCoroutine(SecuenciaFinal());
        }
    }


    IEnumerator SecuenciaFinal()
    {
        nave.DesactivarControl();

        Vector3 inicio = nave.transform.position;
        Vector3 destino = inicio + Vector3.down * descenso;

        while (Vector3.Distance(nave.transform.position, destino) > 0.05f)
        {
            nave.MoverAutomatico(Vector3.down, velocidadDescenso);
            yield return null;
        }

        textoFin.gameObject.SetActive(true);

        float t = 0f;
        while (t < duracionTexto)
        {
            nave.MoverAutomatico(Vector3.right, velocidadDerecha);
            t += Time.deltaTime;
            yield return null;
        }

        textoFin.gameObject.SetActive(false);
        textoGracias.gameObject.SetActive(true);

        t = 0f;
        while (t < duracionTexto)
        {
            nave.MoverAutomatico(Vector3.right, velocidadDerecha);
            t += Time.deltaTime;
            yield return null;
        }

        float a = 0f;
        while (a < 1f)
        {
            a += Time.deltaTime / duracionFade;
            fadeNegro.alpha = a;
            yield return null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

