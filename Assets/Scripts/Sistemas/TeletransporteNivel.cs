using UnityEngine;
using UnityEngine.SceneManagement; // Solo si cambias de escena

public class TeletransporteNivel : MonoBehaviour
{
    [Header("Opcional: Escena a cargar")]
    public string nombreEscenaSiguiente; // Si quieres cambiar de escena

    [Header("Opcional: Posición dentro de la misma escena")]
    public Vector3 posicionDestino; // Si quieres teletransportar dentro de la misma escena
    public bool usarPosicion = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player ha entrado a la zona de teletransporte");

            if (usarPosicion)
            {
                // Teletransportar dentro de la misma escena
                other.transform.position = posicionDestino;
            }
            else if (!string.IsNullOrEmpty(nombreEscenaSiguiente))
            {
                // Cambiar de escena
                SceneManager.LoadScene(nombreEscenaSiguiente);
            }
        }
    }
}
