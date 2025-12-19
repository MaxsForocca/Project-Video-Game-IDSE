using UnityEngine;

public class Bala : MonoBehaviour
{
    [Header("Configuración de Bala")]
    public float velocidad = 20f;
    public int daño = 1;
    public float tiempoDeVida = 2f;

    void Start() 
    {
        Destroy(gameObject, tiempoDeVida);
    }

    void Update()
    {
        transform.Translate(Vector3.right * velocidad * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Enemigo"))
        {
            VidaEnemigo salud = otro.GetComponent<VidaEnemigo>();
            if (salud != null)
            {
                salud.RecibirDaño(daño);
            }
            Destroy(gameObject);
        }
        else if (otro.CompareTag("Obstaculo"))
        {
            Destroy(gameObject);
        }
    }
}