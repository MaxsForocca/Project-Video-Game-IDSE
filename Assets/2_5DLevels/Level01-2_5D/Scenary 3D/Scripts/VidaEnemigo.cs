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
            Morir();
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