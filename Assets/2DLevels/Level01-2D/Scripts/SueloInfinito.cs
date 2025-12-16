using UnityEngine;
using System.Collections.Generic; // Necesario para usar Listas

public class SueloInfinito : MonoBehaviour
{
    public Transform camara;
    public List<GameObject> trozosDeSuelo; // Arrastra tus 3 suelos aquí
    public float longitudDelTrozo = 30f; // ¿Cuánto mide cada bloque de largo? (Cambialo en el inspector)

    void Update()
    {
        // Revisamos cada trozo de suelo en nuestra lista
        foreach (GameObject trozo in trozosDeSuelo)
        {
            // Si la cámara ya pasó este trozo por mucho (se quedó muy atrás)...
            // "camara.x - 35" es un margen de seguridad para que no desaparezca en pantalla
            if (camara.position.x > trozo.transform.position.x + longitudDelTrozo + 10)
            {
                MoverAlFrente(trozo);
            }
        }
    }

    void MoverAlFrente(GameObject trozoMovido)
    {
        // 1. Buscamos cuál es el trozo que está más adelante de todos ahora mismo
        float xMasLejana = 0;

        foreach (GameObject t in trozosDeSuelo)
        {
            if (t.transform.position.x > xMasLejana)
            {
                xMasLejana = t.transform.position.x;
            }
        }

        // 2. Movemos el trozo viejo JUSTO delante del que está más lejos
        Vector3 nuevaPos = new Vector3(xMasLejana + longitudDelTrozo, trozoMovido.transform.position.y, trozoMovido.transform.position.z);
        trozoMovido.transform.position = nuevaPos;
    }
}