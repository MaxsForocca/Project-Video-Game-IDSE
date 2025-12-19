using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidad = 8f;
    public float tiempoMaximoVida = 3f;

    private float direccionX = 1f;
    private float tiempoVida = 0f;

    public void SetDireccion(int dir)
    {
        direccionX = Mathf.Sign(dir);
    }

    void Update()
    {
        transform.position += Vector3.right * direccionX * velocidad * Time.deltaTime;

        tiempoVida += Time.deltaTime;
        if (tiempoVida >= tiempoMaximoVida)
        {
            Destroy(gameObject);
        }
    }
}
