using UnityEngine;
using System.Collections.Generic;
using System.Linq; // Necesario para ordenar listas

public class ControladorSuelos : MonoBehaviour
{
    [Header("Arrastra la Cámara")]
    public Transform camara;

    [Header("Arrastra tus 3 Suelos Padres Aquí")]
    // No importa el orden en que los arrastres, el script los ordena solo
    public List<Transform> misSuelos;

    [Header("Configuración")]
    public float longitudDelSuelo = 30f; // Lo que mide UN bloque (ej. 30)

    // Esto evita el parpadeo. Solo recicla cuando estemos seguros.
    private float distanciaDeReciclaje;

    void Start()
    {
        // 1. Ordenamos la lista por posición X para saber quién es el 1, 2 y 3
        misSuelos = misSuelos.OrderBy(t => t.position.x).ToList();

        // 2. Calculamos cuándo reciclar. 
        // Con 3 suelos, la distancia segura es la longitud de uno completo.
        distanciaDeReciclaje = longitudDelSuelo;
    }

    void Update()
    {
        // --- LOGICA PARA AVANZAR (HACIA DERECHA) ---
        // Miramos al primer suelo de la lista (el de más a la izquierda)
        Transform sueloIzquierdo = misSuelos[0];
        Transform sueloDerecho = misSuelos[misSuelos.Count - 1];

        // Si la cámara ya pasó al suelo izquierdo por mucha distancia...
        if (camara.position.x > sueloIzquierdo.position.x + longitudDelSuelo + (longitudDelSuelo / 2))
        {
            // MOVER UNO POR UNO:
            // Agarramos el suelo de la izquierda y lo ponemos a la derecha del último
            Vector3 nuevaPos = sueloDerecho.position;
            nuevaPos.x += longitudDelSuelo;
            sueloIzquierdo.position = nuevaPos;

            // Actualizamos la lista: El que estaba primero pasa a ser el último
            misSuelos.RemoveAt(0);
            misSuelos.Add(sueloIzquierdo);
        }

        // --- LOGICA PARA RETROCEDER (HACIA IZQUIERDA) ---
        // Si la cámara se alejó hacia la izquierda del suelo derecho...
        else if (camara.position.x < sueloDerecho.position.x - longitudDelSuelo - (longitudDelSuelo / 2))
        {
            // MOVER UNO POR UNO:
            // Agarramos el suelo de la derecha y lo ponemos a la izquierda del primero
            Vector3 nuevaPos = sueloIzquierdo.position;
            nuevaPos.x -= longitudDelSuelo;
            sueloDerecho.position = nuevaPos;

            // Actualizamos la lista: El que estaba último pasa a ser primero
            misSuelos.RemoveAt(misSuelos.Count - 1);
            misSuelos.Insert(0, sueloDerecho);
        }
    }
}