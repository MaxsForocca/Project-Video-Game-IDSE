using UnityEngine;

public class Bala : MonoBehaviour
{
    public float tiempoVida = 5f; // Aumentado a 5 segundos

    private Rigidbody cuerpoRigido;
    private Camera camaraPrincipal;
    private float margenDestruccion = 1f;
    private float velocidad;

    public void Inicializar(float velocidadBala, bool mirandoDerecha)
    {
        velocidad = velocidadBala;

        cuerpoRigido = GetComponent<Rigidbody>();
        camaraPrincipal = Camera.main;

        if (cuerpoRigido != null)
        {
            cuerpoRigido.useGravity = false;
            cuerpoRigido.linearDamping = 0f;
            cuerpoRigido.angularDamping = 0f;

            // Usar velocity directo para movimiento limpio en línea recta
            cuerpoRigido.linearVelocity = (mirandoDerecha ? Vector3.right : Vector3.left) * velocidad;
        }

        Destroy(gameObject, tiempoVida);
    }

    void Update()
    {
        VerificarFueraDeCamara();
    }

    void VerificarFueraDeCamara()
    {
        if (camaraPrincipal == null) return;

        Vector3 posicionEnPantalla = camaraPrincipal.WorldToViewportPoint(transform.position);

        bool fueraDePantalla =
            posicionEnPantalla.x < -margenDestruccion ||
            posicionEnPantalla.x > 1 + margenDestruccion ||
            posicionEnPantalla.y < -margenDestruccion ||
            posicionEnPantalla.y > 1 + margenDestruccion;

        if (fueraDePantalla)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision colision)
    {
        if (!colision.gameObject.CompareTag("Nave"))
        {
            Destroy(gameObject);
        }
    }
}