using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ControladorInfinito : MonoBehaviour
{
    [Header("Arrastra la Cámara")]
    public Transform camara;

    [Header("Arrastra tus 3 Objetos Aquí")]
    public List<Transform> misObjetos;

    [Header("Configuración")]
    public float tamañoDelObjeto = 50f; // ¡CAMBIA ESTO AL ANCHO DE TU IMAGEN!

    void Start()
    {
        // Ordenamos la lista por posición X (Izquierda a Derecha)
        misObjetos = misObjetos.OrderBy(t => t.position.x).ToList();
    }

    void Update()
    {
        Transform objetoIzquierdo = misObjetos[0];
        Transform objetoDerecho = misObjetos[misObjetos.Count - 1];

        // --- AVANZAR (Hacia la Derecha) ---
        // Si la cámara pasó el primer objeto...
        if (camara.position.x > objetoIzquierdo.position.x + tamañoDelObjeto + (tamañoDelObjeto / 2))
        {
            // Movemos el objeto de la izquierda al final de la fila derecha
            Vector3 nuevaPos = objetoDerecho.position;
            nuevaPos.x += tamañoDelObjeto;
            objetoIzquierdo.position = nuevaPos;

            // Actualizamos la lista
            misObjetos.RemoveAt(0);
            misObjetos.Add(objetoIzquierdo);
        }

        // --- RETROCEDER (Hacia la Izquierda) ---
        // Si la cámara se alejó hacia atrás del último objeto...
        else if (camara.position.x < objetoDerecho.position.x - tamañoDelObjeto - (tamañoDelObjeto / 2))
        {
            // Movemos el objeto de la derecha al principio de la izquierda
            Vector3 nuevaPos = objetoIzquierdo.position;
            nuevaPos.x -= tamañoDelObjeto;
            objetoDerecho.position = nuevaPos;

            // Actualizamos la lista
            misObjetos.RemoveAt(misObjetos.Count - 1);
            misObjetos.Insert(0, objetoDerecho);
        }
    }
}