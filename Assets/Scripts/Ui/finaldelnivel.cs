using UnityEngine;
using TMPro;
using System.Collections;

public class FinalNivel : MonoBehaviour
{
    public Nave nave;
    public float velocidadMovimiento = 1.5f;
    public float duracionMovimientoDerecha = 5f;
    public float duracionDescenso = 2f;
    public TextMeshProUGUI textoFin;
    public TextMeshProUGUI textoGracias;
    public CanvasGroup fondoNegro;
    public float duracionTexto = 5f;
    public float duracionFade = 2f;
    public BombaSpawner[] spawners;
    bool iniciado = false;

    void Awake()
    {
        if (nave == null)
            nave = FindFirstObjectByType<Nave>();

        if (textoFin != null)
            textoFin.gameObject.SetActive(false);

        if (textoGracias != null)
            textoGracias.gameObject.SetActive(false);

        if (fondoNegro != null)
            fondoNegro.alpha = 0f;
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
        if (nave == null)
            yield break;

        nave.DesactivarControl();

        if (nave.modeloNave != null)
        {
            Vector3 s = nave.modeloNave.localScale;
            s.x = Mathf.Abs(s.x);
            nave.modeloNave.localScale = s;
            nave.direccionX = 1;
        }

        foreach (var s in spawners)
        {
            if (s != null)
                s.DesactivarSpawner();
        }

        float tiempo = 0f;
        while (tiempo < duracionDescenso)
        {
            nave.MoverAutomatico(Vector3.down, velocidadMovimiento);
            tiempo += Time.deltaTime;
            yield return null;
        }

        tiempo = 0f;
        while (tiempo < duracionMovimientoDerecha)
        {
            nave.MoverAutomatico(Vector3.right, velocidadMovimiento);
            tiempo += Time.deltaTime;
            yield return null;
        }

        float a = 0f;
        while (a < 1f)
        {
            a += Time.deltaTime / duracionFade;
            fondoNegro.alpha = a;
            yield return null;
        }
        fondoNegro.alpha = 1f;

        yield return new WaitForSeconds(5f);

        textoFin.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        textoFin.gameObject.SetActive(false);

        yield return new WaitForSeconds(5f);

        textoGracias.gameObject.SetActive(true);
    }
}
