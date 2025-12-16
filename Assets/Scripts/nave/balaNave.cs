using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidadBala = 30f;
    public float tiempoVida = 3f;

    private Rigidbody cuerpoRigido;
    private Camera camaraPrincipal;
    private float margenDestruccion = 1f;

    void Start()
    {
        cuerpoRigido = GetComponent<Rigidbody>();
        camaraPrincipal = Camera.main;

        if (cuerpoRigido != null)
        {
            cuerpoRigido.useGravity = false;
            cuerpoRigido.linearDamping = 0f;
            cuerpoRigido.angularDamping = 0.5f;

            Vector3 impulsoInicial = Vector3.right * velocidadBala;
            cuerpoRigido.AddForce(impulsoInicial, ForceMode.VelocityChange);
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