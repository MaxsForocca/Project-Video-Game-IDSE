using UnityEngine;

public class ControlDisparo : MonoBehaviour
{
    public GameObject balaPrefab; // Aquí arrastras tu prefab de bala
    public Transform puntoDeDisparo; // Aquí arrastras el objeto vacío de la punta

    void Update()
    {
        // Dispara con la BARRA ESPACIADORA o CLIC IZQUIERDO
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            Disparar();
        }
    }

    void Disparar()
    {
        Instantiate(balaPrefab, puntoDeDisparo.position, puntoDeDisparo.rotation);
    }
}