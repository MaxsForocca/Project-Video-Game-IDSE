using UnityEngine;
using System.Collections;

public class BombaSpawner : MonoBehaviour
{
    public GameObject prefabBomba;
    public Transform puntoSpawn;

    public float tiempoEntreBombas = 0.3f;
    public float tiempoEntreRafagas = 3f;

    public bool dispararHaciaDerecha = true;

    private bool activo = false;
    private Coroutine rutinaDisparo;

    void Start()
    {
        activo = false;
    }

    public void ActivarSpawner()
    {
        if (activo) return;

        activo = true;
        rutinaDisparo = StartCoroutine(DispararRutina());
    }

    public void DesactivarSpawner()
    {
        if (!activo) return;

        activo = false;

        if (rutinaDisparo != null)
        {
            StopCoroutine(rutinaDisparo);
            rutinaDisparo = null;
        }
    }

    IEnumerator DispararRutina()
    {
        while (activo)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject instancia = Instantiate(
                    prefabBomba,
                    puntoSpawn.position,
                    prefabBomba.transform.rotation
                );

                Bomba bomba = instancia.GetComponent<Bomba>();
                if (bomba != null)
                {
                    bomba.SetDireccion(dispararHaciaDerecha);
                }

                yield return new WaitForSeconds(tiempoEntreBombas);
            }

            yield return new WaitForSeconds(tiempoEntreRafagas);
        }
    }
}
