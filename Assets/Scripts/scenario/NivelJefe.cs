using UnityEngine;

public class NivelJefe : MonoBehaviour
{
    public float ancho = 18f;
    public float alto = 8f;

    public Transform puntoIzquierda;
    public Transform puntoCentro;
    public Transform puntoDerecha;

    void Start()
    {
        if (puntoIzquierda == null || puntoCentro == null || puntoDerecha == null)
        {
            CrearPuntosAutomaticos();
        }
        else
        {
            PosicionarPuntos();
        }
    }

    void CrearPuntosAutomaticos()
    {
        GameObject izq = new GameObject("PuntoIzquierda");
        GameObject cen = new GameObject("PuntoCentro");
        GameObject der = new GameObject("PuntoDerecha");

        puntoIzquierda = izq.transform;
        puntoCentro = cen.transform;
        puntoDerecha = der.transform;

        PosicionarPuntos();
    }

    void PosicionarPuntos()
    {
        Vector3 centroArea = transform.position;
        float sueloY = centroArea.y - alto / 2 + 1f;

        puntoIzquierda.position = centroArea + new Vector3(-ancho / 2 + 2f, sueloY, 0);
        puntoCentro.position = centroArea + new Vector3(0, sueloY, 0);
        puntoDerecha.position = centroArea + new Vector3(ancho / 2 - 2f, sueloY, 0);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 centro = transform.position;

        Vector3 esqInfIzq = centro + new Vector3(-ancho / 2, -alto / 2, 0);
        Vector3 esqSupIzq = centro + new Vector3(-ancho / 2, alto / 2, 0);
        Vector3 esqSupDer = centro + new Vector3(ancho / 2, alto / 2, 0);
        Vector3 esqInfDer = centro + new Vector3(ancho / 2, -alto / 2, 0);

        Gizmos.DrawLine(esqInfIzq, esqSupIzq);
        Gizmos.DrawLine(esqSupIzq, esqSupDer);
        Gizmos.DrawLine(esqSupDer, esqInfDer);
        Gizmos.DrawLine(esqInfDer, esqInfIzq);

        if (puntoIzquierda != null && puntoCentro != null && puntoDerecha != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(puntoIzquierda.position, 0.3f);
            Gizmos.DrawSphere(puntoCentro.position, 0.3f);
            Gizmos.DrawSphere(puntoDerecha.position, 0.3f);
        }
    }
}