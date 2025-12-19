    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class Nave : MonoBehaviour
    {
        public float velocidadMovimiento = 5f;
        public double vida = 20.0;

        public GameObject prefabDisparo;
        public Transform puntoDisparo;
        public float tiempoEntreDisparos = 0.25f;
        public Transform modeloNave; // hijo visual de la nave

        private float proximoDisparo = 0f;
        public int direccionX = 1;
        private Rigidbody rb;

        public bool controlActivo = true;


    void Start()
        {
            rb = GetComponent<Rigidbody>();

            if (modeloNave == null && transform.childCount > 0)
            {
                modeloNave = transform.GetChild(0);
            }
        }

        void Update()
    {
        if (controlActivo)
        {
            Mover();
            Disparar();
        }
    }

        void Mover()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Vector3 movimiento = new Vector3(h, v, 0f).normalized;
            rb.MovePosition(rb.position + movimiento * velocidadMovimiento * Time.deltaTime);

            if (modeloNave != null)
            {
                if (h > 0f)
                {
                    direccionX = 1;
                    Vector3 s = modeloNave.localScale;
                    s.x = Mathf.Abs(s.x);
                    modeloNave.localScale = s;
                }
                else if (h < 0f)
                {
                    direccionX = -1;
                    Vector3 s = modeloNave.localScale;
                    s.x = -Mathf.Abs(s.x);
                    modeloNave.localScale = s;
                }
            }
        }

        void Disparar()
        {
            if (Input.GetMouseButton(0) && Time.time >= proximoDisparo)
            {
                GameObject disparo = Instantiate(
                    prefabDisparo,
                    puntoDisparo.position,
                    Quaternion.identity
                );

                Bala bala = disparo.GetComponent<Bala>();
                if (bala != null)
                    bala.SetDireccion(direccionX);

                proximoDisparo = Time.time + tiempoEntreDisparos;
            }
        }

        public void RecibirDanio(double danio)
        {
            vida -= danio;

            if (vida <= 0)
                Destroy(gameObject);
        }
    public void DesactivarControl()
    {
        controlActivo = false;
    }

    public void ActivarControl()
    {
        controlActivo = true;
    }

    public void MoverAutomatico(Vector3 direccion, float velocidad)
    {
        transform.position += direccion.normalized * velocidad * Time.deltaTime;
    }


}
