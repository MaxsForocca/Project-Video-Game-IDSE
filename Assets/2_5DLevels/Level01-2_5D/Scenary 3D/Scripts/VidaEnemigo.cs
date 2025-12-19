using UnityEngine;

public class VidaEnemigo : MonoBehaviour
{
    public int salud = 3;
    public GameObject efectoExplosion;

    public void RecibirDaño(int cantidad)
    {
        salud -= cantidad;

        if (salud <= 0)
        {
            //SUMAR PUNTOS AL MORIR ---
            if (UIManager.Instance != null)
            {
                UIManager.Instance.SumarPuntos(100);
            }

            Destroy(gameObject);
        }
    }

    void Morir()
    {
        if (efectoExplosion != null)
        {
            Instantiate(efectoExplosion, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}