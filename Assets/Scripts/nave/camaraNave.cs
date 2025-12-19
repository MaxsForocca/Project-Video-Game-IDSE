using UnityEngine;

public class CamaraSeguir : MonoBehaviour
{
    public Transform objetivo;
    public float suavizado = 5f;

    private Vector3 posicionInicialCamara;
    private Vector3 posicionInicialObjetivo;

    void Start()
    {
        posicionInicialCamara = transform.position;
        posicionInicialObjetivo = objetivo.position;
    }

    void LateUpdate()
    {
        if (objetivo == null) return;

        Vector3 delta = objetivo.position - posicionInicialObjetivo;
        Vector3 posicionDeseada = posicionInicialCamara + delta;

        transform.position = Vector3.Lerp(
            transform.position,
            posicionDeseada,
            suavizado * Time.deltaTime
        );
    }
}
