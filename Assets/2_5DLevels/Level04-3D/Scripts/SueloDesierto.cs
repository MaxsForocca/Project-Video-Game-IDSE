using UnityEngine;

public class SueloDesierto : MonoBehaviour
{
    [Header("Velocidad del Viento")]
    // X mueve la arena de lado, Y hacia adelante/atrás
    public Vector2 velocidadViento = new Vector2(0.1f, 0f);

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        // Esto mueve la "piel" (textura) del objeto suavemente
        rend.material.mainTextureOffset += velocidadViento * Time.deltaTime;
    }
}